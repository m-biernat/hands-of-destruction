using UnityEngine;
using UnityEngine.Networking;

// This class is used for handling all player related stuff (attributes and methods to change them).
public class Player : NetworkBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float maxStamina = 100f;

    public float baseSpeed = 6f;
    public float sprintSpeed = 10f;

    [SyncVar]
    private float health;

    public float Stamina { get; set; }
    public float Defense { get; set; }
    public float Speed { get; set; }

    public float jumpForce = 5f;

    [SerializeField] private Armor armor;
    public SkinnedMeshRenderer playerMesh;

    void Start()
    {
        health = maxHealth;
        Stamina = maxStamina;
        Speed = baseSpeed;
        SetupArmor();
    }

    // It should go somwhere else propably (but it works fine)
    private void SetupArmor()
    {
        Defense = armor.armorModifier;
        baseSpeed += armor.speedModifier;

        AttachMesh(armor.meshes[0]);
    }

    // And this too...
    private void AttachMesh(SkinnedMeshRenderer itemMesh)
    {
        if (itemMesh)
        {
            SkinnedMeshRenderer attachedMesh = Instantiate(itemMesh);
            attachedMesh.transform.parent = playerMesh.transform;

            attachedMesh.bones = playerMesh.bones;
            attachedMesh.rootBone = playerMesh.rootBone;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
