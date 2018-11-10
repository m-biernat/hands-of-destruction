using UnityEngine;

[RequireComponent(typeof(PlayerMotor), typeof(PlayerMovement), typeof(CameraManager))]
public class PlayerController : MonoBehaviour
{
    private PlayerMotor motor;
    private PlayerMovement movement;
    private CameraManager cameras;

    private float holdTime = 0f;

    void Start ()
    {
        motor = GetComponent<PlayerMotor>();
        movement = GetComponent<PlayerMovement>();
        cameras = GetComponent<CameraManager>();
}

    void Update()
    {   
        float xAxisMovement = Input.GetAxisRaw("Horizontal");
        float zAxisMovement = Input.GetAxisRaw("Vertical");

        motor.Move(movement.Velocity(xAxisMovement, zAxisMovement));

        float yAxisRotation = Input.GetAxisRaw("Mouse X");
        motor.RotateCameraY(cameras.CalculateRotationY(yAxisRotation));

        float xAxisRotation = Input.GetAxisRaw("Mouse Y");
        motor.RotateCameraX(cameras.CalculateRotationX(xAxisRotation));

        float zAxisDistance = Input.GetAxis("Mouse ScrollWheel");
        cameras.ChangeDistance(zAxisDistance);

        if (Input.GetButtonDown("Camera Toggle"))
        { cameras.Toggle(); }

        if (movement.IsGrounded())
        {
            Vector3 jumpForce = Vector3.zero;
            if (Input.GetButtonDown("Jump"))
            {
                jumpForce = movement.JumpForce();
            }
            motor.Jump(jumpForce);

            movement.Run();
            if (Input.GetButton("Sprint") && (zAxisMovement > 0))
            { movement.Sprint(); }

            if (Input.GetButtonDown("Dodge") && !(zAxisMovement > 0))
            { movement.Dodge(); }

            if (Input.GetButton("Crouch"))
            { movement.Crouch(); }
        }

        if ((Input.GetButton("Mouse Left"))) { holdTime += Time.deltaTime; }

        if (Input.GetButtonUp("Mouse Left"))
        {
            if (holdTime > 0f && holdTime <= .2f) Debug.Log("Light Attack");
            if (holdTime >= .5f) Debug.Log("Heavy Attack");
            holdTime = 0f;
        }

        if (Input.GetButton("Mouse Right"))
        {
            //Debug.Log("Block");
            holdTime = 0f;
            if (Input.GetButtonDown("Mouse Left"))
            { Debug.Log("Counterattack"); }
        }
    }
}
