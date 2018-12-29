using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

// This class is used for handling all player related stuff (attributes and methods to change them).
public class Player : NetworkBehaviour
{
    [SyncVar] public string playerName;

    [SyncVar] public byte magicID;
    [SyncVar] public byte armorID;

    [SyncVar] public byte teamID;

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
    public float magickaRegen = 10f;

    public float sprintCost = 20f;

    public float jumpForce = 5f;


    [SerializeField] private ArmorManager armorManager;
    [SerializeField] private SkinnedMeshRenderer playerMesh;
    private Armor armor;

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;

    private Animator animator;

    public static byte currentTeamID = 0;

    public void Setup()
    {
        wasEnabled = new bool[disableOnDeath.Length];
        for (int i = 0; i < wasEnabled.Length; i++)
        {
            wasEnabled[i] = disableOnDeath[i].enabled;
        }
        
        SetupArmor();
        SetDefaults();

        animator = GetComponent<Animator>();
    }

    [ClientRpc]
    public void RpcTakeDamage(float damage)
    {
        if (IsAlive)
        {
            Health -= damage;
            animator.SetTrigger("TakeDamage");
        }
        if (Health <= 0f)
        {
            Die();
        }
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
        animator.SetTrigger("Death");

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

    private void SetDefaults()
    {
        IsAlive = true;

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

    private void SetupArmor()
    {
        armor = armorManager.GetArmor(armorID);

        maxHealth += (maxHealth * armor.healthModifier);
        maxStamina += (maxStamina * armor.staminaModifier);
        maxMagicka += (maxMagicka * armor.magickaModifier);

        staminaRegen += (staminaRegen * armor.staminaRegenModifier);
        magickaRegen += (magickaRegen * armor.magickaRegenModifier);

        runSpeed += (runSpeed * armor.speedModifier);
        sprintSpeed += (sprintSpeed * armor.speedModifier);
        walkSpeed += (walkSpeed * armor.speedModifier);

        armorManager.AttachMesh(playerMesh, armor.mesh);
    }

    public void SetCurrentTeamID()
    {
        if (teamID != 0)
            currentTeamID = teamID;
        else
            currentTeamID = 3;
    }
}
