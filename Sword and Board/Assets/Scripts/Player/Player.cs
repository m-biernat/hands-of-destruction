using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

// This class is used for handling all player related stuff (attributes and methods to change them).
public class Player : NetworkBehaviour
{
    [SyncVar] private bool _isAlive;
    public bool IsAlive { get { return _isAlive; } protected set { _isAlive = value; } }

    private float maxHealth = 100f;
    private float maxStamina = 100f;
    private float maxMagicka = 100f;

    public float GetMaxHealth() { return maxHealth; }
    public float GetMaxStamina() { return maxStamina; }
    public float GetMaxMagicka() { return maxMagicka; }

    [SyncVar] private float _health;
    private float _stamina, _magicka;

    public float Health
    {
        get { return _health; }
        set { _health = Mathf.Clamp(value, 0, maxHealth); }
    }
    public float Stamina
    {
        get { return _stamina; }
        set { _stamina = Mathf.Clamp(value, 0, maxStamina); }
    }
    public float Magicka
    {
        get { return _magicka; }
        set { _magicka = Mathf.Clamp(value, 0, maxMagicka); }
    }

    public float Speed { get; set; }

    public float runSpeed = 6f;
    public float sprintSpeed = 10f;
    public float walkSpeed = 2f;

    public float staminaRegen = 10f;
    public float sprintCost = 20f;

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
        runSpeed += armor.speedModifier;

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
        if (IsAlive) Health -= damage;
        if (Health <= 0f) Die();
    }

    private void Die()
    {
        IsAlive = false;

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
        IsAlive = true;

        SetupArmor();

        Health = maxHealth;
        Stamina = maxStamina;
        Magicka = maxMagicka;

        Speed = runSpeed;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        Collider col = GetComponent<Collider>();
        col.enabled = true;
        col.attachedRigidbody.useGravity = true;
    }
}
