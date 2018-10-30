using UnityEngine;

[RequireComponent(typeof(PlayerMotor), typeof(PlayerMovement), typeof(CameraController))]
public class PlayerController : MonoBehaviour
{
    private PlayerMotor motor;
    private PlayerMovement movement;
    private PlayerAttributes attribute;
    private CameraController playerCamera;

    void Start ()
    {
        motor = GetComponent<PlayerMotor>();
        movement = GetComponent<PlayerMovement>();
        attribute = GetComponent<PlayerAttributes>();
        playerCamera = GetComponent<CameraController>();
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
        {
            playerCamera.Toggle();
        }

        Vector3 jumpForce = Vector3.zero;
        if (Input.GetButtonDown("Jump"))
        {
            jumpForce = Vector3.up * attribute.jumpForce;
        }
        motor.Jump(jumpForce);

        if (Input.GetButtonDown("Sprint") && (zAxisMovement > 0))
        { Debug.Log("Sprint!"); }

        if (Input.GetButtonDown("Dodge") && !(zAxisMovement > 0))
        { Debug.Log("Dodge!"); }

        if (Input.GetButtonDown("Crouch"))
        { Debug.Log("Crouch!"); }
    }
}
