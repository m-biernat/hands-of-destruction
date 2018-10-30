using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    [SerializeField]
    private float speed = 8f;

    public float jumpForce = 6f;

    public float getSpeed () { return speed; }
}
