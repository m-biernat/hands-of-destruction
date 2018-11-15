using UnityEngine;

// This class is used to take all input from the player and execute methods based on that input.
[RequireComponent(typeof(PlayerMotor), typeof(PlayerMovement), typeof(CameraManager))]
public class PlayerController : MonoBehaviour
{
    private PlayerMotor motor;
    private PlayerMovement movement;
    private CameraManager cameras;

    private PlayerCombat combat;

    private float holdTime = 0f;

    void Start ()
    {
        motor = GetComponent<PlayerMotor>();
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
        motor.Move(movement.Velocity(xAxisMovement, zAxisMovement));

        // Those two takes input from mouse X and Y axis.
        // And rotate rigidbody and cameras.
        float yAxisRotation = Input.GetAxisRaw("Mouse X");
        motor.RotateCameraY(cameras.CalculateRotationY(yAxisRotation));

        float xAxisRotation = Input.GetAxisRaw("Mouse Y");
        motor.RotateCameraX(cameras.CalculateRotationX(xAxisRotation));

        // Takes input from scrollwheel and changes camera distance.
        float zAxisDistance = Input.GetAxis("Mouse ScrollWheel");
        cameras.ChangeDistance(zAxisDistance);

        // Toggles active camera
        if (Input.GetButtonDown("Camera Toggle")) cameras.Toggle();

        // Perform actions only if player is not jumping.
        if (movement.IsGrounded())
        {
            // Set jump vector if player is jumping.
            Vector3 jumpForce = Vector3.zero;
            if (Input.GetButtonDown("Jump"))
            {
                jumpForce = movement.JumpForce();
            }
            motor.Jump(jumpForce);

            // Sets player into running mode.
            movement.Run();

            // Sets player into sprint mode only if going forward.
            if (Input.GetButton("Sprint") && (zAxisMovement > 0))
            { movement.Sprint(); }

            // Dodge if player is not heading forward.
            if (Input.GetButtonDown("Dodge") && !(zAxisMovement > 0))
            { movement.Dodge(); }

            // This will be changed to walk propably.
            if (Input.GetButton("Crouch"))
            { movement.Crouch(); }
        }

        if ((Input.GetButton("Mouse Left"))) { holdTime += Time.deltaTime; }

        if (Input.GetButtonUp("Mouse Left"))
        {
            if (holdTime > 0f && holdTime <= .2f) combat.Attack(); //Debug.Log("Light Attack");
            if (holdTime >= .5f) Debug.Log("Heavy Attack");
            holdTime = 0f;
        }

        if (Input.GetButton("Mouse Right"))
        {
            //Debug.Log("Block");
            holdTime = 0f;
            if (Input.GetButtonDown("Mouse Left"))
            {
                Debug.Log("CounterAttack");
            }
        }
    }
}
