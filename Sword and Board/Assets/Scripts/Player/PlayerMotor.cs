using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CameraController))]
public class PlayerMotor : MonoBehaviour
{
    private Rigidbody rb;
    private CameraController playerCamera;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotationY = Vector3.zero;
    private Vector3 rotationX = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = GetComponent<CameraController>();
    }

    public void Move(Vector3 velocity) { this.velocity = velocity; }

    public void RotateCameraY(Vector3 rotation) { this.rotationY = rotation; }

    public void RotateCameraX(Vector3 rotation) { this.rotationX = rotation; }

    void FixedUpdate()
    {
        ApplyMovement();
        ApplyRotation();    
    }

    void ApplyMovement()
    {
        if(velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    void ApplyRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotationY));
        playerCamera.Rotate(rotationX);
    }
}
