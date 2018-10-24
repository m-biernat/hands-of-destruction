using UnityEngine;

[RequireComponent(typeof(PlayerMotor), typeof(PlayerMovement), typeof(CameraController))]
public class PlayerController : MonoBehaviour
{
    private PlayerMotor motor;
    private PlayerMovement movement;
    private CameraController playerCamera;

    void Start ()
    {
        motor = GetComponent<PlayerMotor>();
        movement = GetComponent<PlayerMovement>();
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

        float zAxisDistance = Input.GetAxisRaw("Mouse ScrollWheel");
        Debug.Log(zAxisDistance);

        if (Input.GetButtonDown("Camera Toggle")) { playerCamera.Toggle(); }
    }
}
