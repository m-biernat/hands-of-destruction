using UnityEngine;

// This class is used for storing all methods related to player movement.
[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    private Player player;

    private Collider col;
    private float distToGround;

    void Start()
    {
        player = GetComponent<Player>();

        col = GetComponent<CapsuleCollider>();
        distToGround = col.bounds.extents.y;
    } 

    // Calculates vector for movement velocity.
    public Vector3 Velocity(float xAxisMovement, float zAxisMovement)
    {
        Vector3 moveHorizontal = transform.right * xAxisMovement;
        Vector3 moveVertical = transform.forward * zAxisMovement;

        return (moveHorizontal + moveVertical).normalized * player.Speed;
    }

    // Returns a jump force.
    public Vector3 JumpForce()
    {
        return Vector3.up * player.jumpForce;
    }

    // Sets player in run state (changes it's speed).
    public void Run()
    {
        player.Speed = player.baseSpeed;
    }

    // Sets player in sprint state (changes it's speed).
    public void Sprint()
    {
        player.Speed = player.sprintSpeed;
    }

    // #Waiting for implementation.
    public void Dodge()
    {
        Debug.Log("Dodge!");
    }

    // #Waiting for implementation.
    public void Crouch()
    {
        Debug.Log("Crouch!");
    }

    // Checks if player is grounded by firing a raycast to the ground.
    // This might be changed to CapsuleCheck in the futute.
    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround + .5f);
    }
}
