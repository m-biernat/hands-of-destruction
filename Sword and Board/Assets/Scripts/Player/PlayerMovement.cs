using UnityEngine;

// This class is used for storing all methods related to player movement.
[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    private Player player;

    private Collider col;
    private float distToGround;

    private float sprintTimer = 0f;
    private float staminaRegenTimer = 0f;

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
    // And regenerates stamina every second.
    public void Run()
    {
        player.Speed = player.runSpeed;
        if (player.Stamina < 100f)
        {
            sprintTimer = 0f;
            staminaRegenTimer += Time.deltaTime;
            if (staminaRegenTimer >= 1f)
            {
                player.Stamina += player.staminaRegen;
                staminaRegenTimer = 0f;
            }
        }
    }

    // Sets player in sprint state (changes it's speed).
    // And decreases stamina every second.
    public void Sprint()
    {
        staminaRegenTimer = -1f;
        sprintTimer += Time.deltaTime;
        if(sprintTimer >= 1f)
        {
            player.Stamina -= player.sprintCost;
            sprintTimer = 0f;
        }     
        player.Speed = player.sprintSpeed;
    }

    // Checks if player has enough stamina to sprint.
    public bool CanSprint()
    {
        return (player.Stamina - player.sprintCost >= 0f);
    }

    // #Waiting for implementation.
    public void Dodge()
    {
        if (player.Stamina - 40f >= 0f)
        {
            player.Speed = player.sprintSpeed * 4;
            player.Stamina -= 40f;
        }
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
