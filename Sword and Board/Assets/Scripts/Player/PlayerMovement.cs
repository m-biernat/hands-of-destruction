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

        return (moveHorizontal + moveVertical).normalized * attribute.getSpeed();
    }
}
