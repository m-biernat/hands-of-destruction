using UnityEngine;

// This class is used to take all input from the player and execute methods based on that input.
[RequireComponent(typeof(Rigidbody), typeof(PlayerMovement), typeof(CameraManager))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    private PlayerMovement movement;
    private CameraManager cameras;
    private PlayerCombat combat;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotationY = Vector3.zero;
    private Vector3 rotationX = Vector3.zero;
    private Vector3 jumpForce = Vector3.zero;

    private float holdTime = 0f;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();

        movement = GetComponent<PlayerMovement>();
        cameras = GetComponent<CameraManager>();
        combat = GetComponent<PlayerCombat>();
    }

    void Update()
    {   
        // Take movement input from the player.
        float xAxisMovement = Input.GetAxisRaw("Horizontal");
        float zAxisMovement = Input.GetAxisRaw("Vertical");

        // Based on this input change rigidbody position in game world.
        velocity = movement.Velocity(xAxisMovement, zAxisMovement);

        // Those two takes input from mouse X and Y axis.
        // And rotate rigidbody and cameras.
        float yAxisRotation = Input.GetAxisRaw("Mouse X");
        rotationY = cameras.CalculateRotationY(yAxisRotation);

        float xAxisRotation = Input.GetAxisRaw("Mouse Y");
        rotationX = cameras.CalculateRotationX(xAxisRotation);   

        // Takes input from scrollwheel and changes camera distance.
        float zAxisDistance = Input.GetAxis("Mouse ScrollWheel");
        cameras.ChangeDistance(zAxisDistance);

        // Toggles active camera
        if (Input.GetButtonDown("Camera Toggle")) cameras.Toggle();

        // Perform actions only if player is not jumping.
        if (movement.IsGrounded())
        {
            // Set jump vector if player is jumping.
            jumpForce = Vector3.zero;
            if (Input.GetButtonDown("Jump"))
            { jumpForce = movement.JumpForce(); }

            // Sets player into sprint mode only if going forward.
            if (Input.GetButton("Sprint") && (zAxisMovement > 0) && movement.CanSprint())
            { movement.Sprint(); }
            else
            { movement.Run(); }

            // Dodge if player is not heading forward.
            if (Input.GetButtonDown("Dodge") && !(zAxisMovement > 0) 
                && (xAxisMovement != 0 || zAxisMovement != 0))
            { movement.Dodge(); }

            // This will be changed to walk propably.
            if (Input.GetButton("Crouch") && !Input.GetButton("Sprint"))
            { movement.Crouch(); }
        }

        if ((Input.GetButton("Mouse Left"))) { holdTime += Time.deltaTime; }

        if (Input.GetButtonUp("Mouse Left"))
        {
            if (holdTime > 0f && holdTime <= .2f)
            {
                combat.MainAttack();
            }
            if (holdTime >= .5f)
            {
                combat.SpecialAttack();
            }
            holdTime = 0f;
        }

        if (Input.GetButton("Mouse Right"))
        {
            combat.Block();
            movement.Walk();
            holdTime = 0f;
            if (Input.GetButtonDown("Mouse Left"))
            {
                combat.CounterAttack();
            }
        }
    }

    void FixedUpdate()
    {
        ApplyMovement();
        ApplyRotation();
    }

    // Apply movement to the rigidbody if there is any change.
    private void ApplyMovement()
    {
        if (velocity != Vector3.zero)
        { rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime); }

        if (jumpForce != Vector3.zero && movement.IsGrounded())
        {
            rb.AddForce(jumpForce, ForceMode.Impulse);

            // This prevents from shooting into the universe.
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, 0f);
        }
    }

    // Apply rotation to rigidbody and cameras component.
    private void ApplyRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotationY));
        cameras.Rotate(rotationX);
    }
}
