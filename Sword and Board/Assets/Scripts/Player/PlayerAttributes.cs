using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    public float getSpeed () { return speed; }
}
