using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

// This class is used for handling all player related stuff (attributes and methods to change them).
public class Player : NetworkBehaviour
{
    [SyncVar] private bool _isAlive;
    public bool isAlive { get { return _isAlive; } protected set { _isAlive = value; } }

    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float maxStamina = 100f;

    public float baseSpeed = 6f;
    public float sprintSpeed = 10f;

    [SyncVar]
    private float health;

    public float Stamina { get; set; }
    public float Speed { get; set; }

    public float jumpForce = 5f;

    [SerializeField] private Armor armor;
    public SkinnedMeshRenderer playerMesh;

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;

    public void Setup()
    {
        wasEnabled = new bool[disableOnDeath.Length];
        for (int i = 0; i < wasEnabled.Length; i++)
        {
            wasEnabled[i] = disableOnDeath[i].enabled;
        }

        SetDefaults();
    }

    // It should go somwhere else propably (but it works fine)
    private void SetupArmor()
    {
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

    [ClientRpc]
    public void RpcTakeDamage(float damage)
    {
        if (isAlive) health -= damage;
        if (health <= 0) Die();
    }

    private void Die()
    {
        isAlive = false;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }

        Collider col = GetComponent<Collider>();
        col.attachedRigidbody.useGravity = false;
        col.enabled = false;     

        Debug.Log(transform.name + " died.");

        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(3f);

        SetDefaults();
        Transform spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
    }

    public void SetDefaults()
    {
        isAlive = true;

        health = maxHealth;
        Stamina = maxStamina;
        Speed = baseSpeed;

        SetupArmor();

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        Collider col = GetComponent<Collider>();
        col.enabled = true;
        col.attachedRigidbody.useGravity = true;
    }
}
