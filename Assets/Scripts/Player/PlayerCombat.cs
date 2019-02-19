using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerCombat : NetworkBehaviour
{
    private GameObject cam;
    private PlayerAnimation animate;

    //private float tempRange = 10f;

    //[SerializeField] private LayerMask mask;

    private Magic magic;

    public GameObject spellPrefab;
    public Transform spellSpawnPoint;

    void Start()
    {
        cam = GetComponent<CameraManager>().GetCameras();
        animate = GetComponent<PlayerAnimation>();
    }

    public void MainAttack()
    {
        if (animate.HasAnimationsEnded())
        {
            animate.Trigger("MainAttack");
            StartCoroutine(Attack(spellPrefab, .5f, 18f, 2f));
        }
    }

    public void SpecialAttack()
    {
        if (animate.HasAnimationsEnded())
        {
            animate.Trigger("SpecialAttack");
            StartCoroutine(Attack(spellPrefab, 1f, 12f, 2f));
        }   
    }

    public void Block()
    {
        animate.SetBlock(true);
    }

    public IEnumerator Attack(GameObject spellPrefab, float delay, float speed, float duration)
    {
        yield return new WaitForSeconds(delay);

        GameObject spell = Instantiate(spellPrefab, spellSpawnPoint.position, cam.transform.rotation);

        spell.GetComponent<Rigidbody>().velocity = spell.transform.forward * speed;

        Destroy(spell, duration);
    }

    /*
    [Client]
    public void Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, tempRange, mask))
        {
            if (hit.collider.tag == "Player" )
            {
                byte hitTeamID = hit.collider.GetComponent<Player>().teamID;
                byte playerTeamID = GetComponent<Player>().teamID;

                if (hitTeamID != playerTeamID || hitTeamID == 0)
                    CmdPlayerHit(hit.collider.name, 10f, transform.name);
            }
        }
    }

    [Command]
    private void CmdPlayerHit(string playerID, float damage, string sourceID)
    {
        Player player = GameManager.GetPlayer(playerID);
        player.RpcTakeDamage(damage, sourceID);

        Debug.Log(playerID + " hit once.");
    }
    */
}
