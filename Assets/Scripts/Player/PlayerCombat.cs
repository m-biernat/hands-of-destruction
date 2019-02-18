using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerCombat : NetworkBehaviour
{
    private GameObject cam;
    private PlayerAnimation animate;

    private float tempRange = 10f;

    [SerializeField] private LayerMask mask;

    public GameObject magicPrefab;
    public Transform magicSpawn;

    void Start()
    {
        cam = GetComponent<CameraManager>().GetCameras();
        animate = GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward) * tempRange, Color.green);
    }

    public void MainAttack()
    {
        if (!animate.HasAnimationsEnded())
        {
            animate.Trigger("MainAttack");
            StartCoroutine(Attack());
        }
    }

    public void SpecialAttack()
    {
        if (!animate.HasAnimationsEnded())
        {
            animate.Trigger("SpecialAttack");
            StartCoroutine(Attack());
        }   
    }

    public void Block()
    {
        animate.SetBlock(true);
    }

    public IEnumerator Attack()
    {
        yield return new WaitForSeconds(.5f);

        GameObject magic = Instantiate(magicPrefab, magicSpawn.position, cam.transform.rotation);

        magic.GetComponent<Rigidbody>().velocity = magic.transform.forward * 12f;

        Destroy(magic, 2);
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
