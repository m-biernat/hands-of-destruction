using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //public float maxHealth = 100f;
    //public float health;

    [SerializeField]
    private float maxStamina = 100f;
    public float Stamina { get; set; }

    public float baseSpeed = 8f;
    public float sprintSpeed = 16f;
    public float speed;

    public float jumpForce = 6f;

    void Start()
    {
        //health = maxHealth;
        Stamina = maxStamina;
        speed = baseSpeed;
    }
}
