using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class PlayerMovement : MonoBehaviour
{
    private PlayerManager player;

    private Collider col;
    private float distToGround;

    void Start()
    {
        player = GetComponent<PlayerManager>();

        col = GetComponent<CapsuleCollider>();
        distToGround = col.bounds.extents.y;
    } 

    public Vector3 Velocity(float xAxisMovement, float zAxisMovement)
    {
        Vector3 moveHorizontal = transform.right * xAxisMovement;
        Vector3 moveVertical = transform.forward * zAxisMovement;

        return (moveHorizontal + moveVertical).normalized * player.speed;
    }

    public Vector3 JumpForce()
    {
        return Vector3.up * player.jumpForce;
    }

    public void Run()
    {
        player.speed = player.baseSpeed;
    }

    public void Sprint()
    {
        player.speed = player.sprintSpeed;
    }

    public void Dodge()
    {
        Debug.Log("Dodge!");
    }

    public void Crouch()
    {
        Debug.Log("Crouch!");
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround + 0.5f);
    }
}
