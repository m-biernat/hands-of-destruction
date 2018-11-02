using UnityEngine;

[RequireComponent(typeof(PlayerAttributes))]
public class PlayerMovement : MonoBehaviour
{
    private PlayerAttributes attribute;

    void Start()
    {
        attribute = GetComponent<PlayerAttributes>();
    } 

    public Vector3 Velocity(float xAxisMovement, float zAxisMovement)
    {
        Vector3 moveHorizontal = transform.right * xAxisMovement;
        Vector3 moveVertical = transform.forward * zAxisMovement;

        return (moveHorizontal + moveVertical).normalized * attribute.speed;
    }

    public Vector3 JumpForce()
    {
        return Vector3.up * attribute.jumpForce;
    }

    public void Run()
    {
        attribute.speed = attribute.baseSpeed;
    }

    public void Sprint()
    {
        attribute.speed = attribute.sprintSpeed;
    }

    public void Dodge()
    {
        Debug.Log("Dodge!");
    }

    public void Crouch()
    {
        Debug.Log("Crouch!");
    }
}
