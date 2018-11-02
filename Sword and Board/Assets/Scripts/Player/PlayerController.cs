using UnityEngine;

[RequireComponent(typeof(PlayerMotor), typeof(PlayerMovement), typeof(CameraController))]
public class PlayerController : MonoBehaviour
{
    private PlayerMotor motor;
    private PlayerMovement movement;
    private CameraController playerCamera;

    private Collider col;
    private float distToGround;

    void Start ()
    {
        motor = GetComponent<PlayerMotor>();
        movement = GetComponent<PlayerMovement>();
        playerCamera = GetComponent<CameraController>();

        col = GetComponent<CapsuleCollider>();
        distToGround = col.bounds.extents.y;
    }

    void Update()
    {   
        float xAxisMovement = Input.GetAxisRaw("Horizontal");
        float zAxisMovement = Input.GetAxisRaw("Vertical");

        motor.Move(movement.Velocity(xAxisMovement, zAxisMovement));

        float yAxisRotation = Input.GetAxisRaw("Mouse X");
        motor.RotateCameraY(playerCamera.CalculateRotationY(yAxisRotation));

        float xAxisRotation = Input.GetAxisRaw("Mouse Y");
        motor.RotateCameraX(playerCamera.CalculateRotationX(xAxisRotation));

        float zAxisDistance = Input.GetAxis("Mouse ScrollWheel");
        playerCamera.ChangeDistance(zAxisDistance);

        if (Input.GetButtonDown("Camera Toggle"))
        { playerCamera.Toggle(); }

        if(IsGrounded())
        {
            Vector3 jumpForce = Vector3.zero;
            if (Input.GetButtonDown("Jump"))
            { jumpForce = movement.JumpForce(); }
            motor.Jump(jumpForce);

            movement.Run();
            if (Input.GetButton("Sprint") && (zAxisMovement > 0))
            { movement.Sprint(); }

            if (Input.GetButtonDown("Dodge") && !(zAxisMovement > 0))
            { movement.Dodge(); }

            if (Input.GetButton("Crouch"))
            { movement.Crouch(); }
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround + 0.5f);
    }
}
