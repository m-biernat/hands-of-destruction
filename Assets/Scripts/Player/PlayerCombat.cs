using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerCombat : NetworkBehaviour
{
    private Player player;
    private GameObject cam;
    private PlayerAnimation animate;

    private Magic magic;

    public Transform spellSpawnPoint;

    private readonly float blockMagickaCost = 10f;

    private float blockTimer = 0f, magickaRegenTimer = 0f;

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

    public void Block()
    {
        if ((player.Magicka - blockMagickaCost) >= 0)
        {
            animate.SetBlock(true);

            blockTimer += Time.deltaTime;
            if (blockTimer >= 1f)
            {
                player.Magicka -= blockMagickaCost;
                blockTimer = 0f;
            }

            lastBlockTime = Time.time;
            // TODO: Enable collider and gfx for block
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

        if (attackType == "MainAttack")
            spellPrefab = magic.mainAttack;
        if (attackType == "SpecialAttack")
            spellPrefab = magic.specialAttack;

        GameObject spell = Instantiate(spellPrefab, spellSpawnPoint.position, cam.transform.rotation);

        spell.GetComponent<Rigidbody>().velocity = spell.transform.forward * velocity;
        spell.GetComponent<Spell>().SetSpell(GetComponent<Player>(), damage);

        NetworkServer.Spawn(spell);

        Destroy(spell, duration);
    }

    public void SetMagic(Magic magic)
    {
        this.magic = magic;
    }
}
