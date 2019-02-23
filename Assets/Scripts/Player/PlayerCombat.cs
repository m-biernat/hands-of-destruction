using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerCombat : NetworkBehaviour
{
    private Player player;
    private GameObject cam;
    private PlayerAnimation animate;

    private Magic magic;

    [SerializeField] private Transform spellSpawnF;
    [SerializeField] private Transform spellSpawnH;

    public GameObject magicShield;

    public bool isBlockActive = false;

    private readonly float blockMagickaCost = 20f;

    public float blockTimer = 0f;
    private float magickaRegenTimer = 0f;

    private float lastAttackTime, lastSpecialAttackTime, lastBlockTime;
    private const float T_0 = .9f, T_1 = 1.2f;

    void Start()
    {
        player = GetComponent<Player>();
        cam = GetComponent<CameraManager>().GetCameras();
        animate = GetComponent<PlayerAnimation>();
        lastAttackTime = lastSpecialAttackTime = lastBlockTime = Time.time;
    }

    public void MainAttack()
    {
        if ((Time.time - lastAttackTime) >= T_0 && (Time.time - lastSpecialAttackTime) >= T_1)
        {
            animate.Trigger("MainAttack");
            StartCoroutine(DelayAttack(.4f, "MainAttack", magic.mainAttackDamage, 
                magic.mainAttackSpeed, magic.mainAttackDuration));
            lastAttackTime = Time.time;
        }
    }

    public void SpecialAttack()
    {
        if (!((Time.time - lastAttackTime) >= T_0 && (Time.time - lastSpecialAttackTime) >= T_1)) return;

        if ((player.Magicka - magic.specialAttackMagickaCost) >= 0f)
        {
            animate.Trigger("SpecialAttack");
            StartCoroutine(DelayAttack(.75f, "SpecialAttack", magic.specialAttackDamage,
                magic.specialAttackSpeed, magic.specialAttackDuration));
            player.Magicka -= magic.specialAttackMagickaCost;
            lastSpecialAttackTime = Time.time;
        }
    }

    public void Block(bool active)
    {
        if (!active)
        {
            if (!isBlockActive) CmdToggleMagicShield(false);
            return;
        }

        if ((player.Magicka - blockMagickaCost) >= 0)
        {
            animate.SetBlock(true);

            if (blockTimer == 0f)
                player.Magicka -= blockMagickaCost;

            blockTimer += Time.deltaTime;
            if (blockTimer >= 1f) blockTimer = 0f;

            lastBlockTime = Time.time;

            if (!magicShield.activeSelf)
                StartCoroutine(ToggleMagicShield(true, .25f));
        }
        else
        {
            if (magicShield.activeSelf)
                CmdToggleMagicShield(false);
        }
    }

    public void RegenerateMagicka()
    {
        if (player.Magicka < player.GetMaxMagicka())
        {
            if (!((Time.time - lastSpecialAttackTime) >= 2f && (Time.time - lastBlockTime) >= .5f)) return;

            magickaRegenTimer += Time.deltaTime;
            if (magickaRegenTimer >= 1f)
            {
                player.Magicka += player.magickaRegen;
                magickaRegenTimer = 0f;
            }
        }
    }

    public IEnumerator DelayAttack(float delay, string attackType, float damage, float velocity, float duration)
    {
        yield return new WaitForSeconds(delay);
        CmdAttack(attackType, damage, velocity, duration);
    }

    [Command]
    public void CmdAttack(string attackType, float damage, float velocity, float duration)
    {
        GameObject spellPrefab = null;
        Transform spellSpawn = null;

        if (attackType == "MainAttack")
        {
            spellPrefab = magic.mainAttack;
            spellSpawn = spellSpawnH;
        }
            
        if (attackType == "SpecialAttack")
        {
            spellPrefab = magic.specialAttack;
            spellSpawn = spellSpawnF;
        }

        GameObject spell = Instantiate(spellPrefab, spellSpawn.position, cam.transform.rotation);

        spell.GetComponent<Rigidbody>().velocity = spell.transform.forward * velocity;
        spell.GetComponent<Spell>().SetSpell(GetComponent<Player>(), damage);

        NetworkServer.Spawn(spell);

        Destroy(spell, duration);
    }

    private IEnumerator ToggleMagicShield(bool active, float delay)
    {
        yield return new WaitForSeconds(delay);
        CmdToggleMagicShield(active);
    }

    [Command]
    public void CmdToggleMagicShield(bool active)
    {
        RpcToggleMagicShield(active);
    }

    [ClientRpc]
    private void RpcToggleMagicShield(bool active)
    {
        magicShield.SetActive(active);
    }

    public void SetMagic(Magic magic)
    {
        this.magic = magic;
    }
}
