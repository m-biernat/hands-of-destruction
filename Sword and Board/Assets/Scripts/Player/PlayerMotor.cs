using UnityEngine;

// This class is used to apply different operations to rigidbody.
[RequireComponent(typeof(Rigidbody), typeof(CameraManager))]
public class PlayerMotor : MonoBehaviour
{
    private Rigidbody rb;
    private CameraManager cameras;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotationY = Vector3.zero;
    private Vector3 rotationX = Vector3.zero;
    private Vector3 jumpForce = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameras = GetComponent<CameraManager>();
    }

    // Those methods are used for taking data from controller.
    public void Move(Vector3 velocity) { this.velocity = velocity; }

    public void RotateCameraY(Vector3 rotation) { this.rotationY = rotation; }

    public void RotateCameraX(Vector3 rotation) { this.rotationX = rotation; }

    public void Jump(Vector3 jumpForce) { this.jumpForce = jumpForce; }

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
        if (jumpForce != Vector3.zero)
        { rb.AddForce(jumpForce, ForceMode.Impulse); }
    }

    // Apply rotation to rigidbody and cameras component.
    private void ApplyRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotationY));
        cameras.Rotate(rotationX);
    }
}
