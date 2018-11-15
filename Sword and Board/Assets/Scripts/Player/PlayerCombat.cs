using UnityEngine;
using UnityEngine.Networking;

public class PlayerCombat : NetworkBehaviour
{
    private Camera cam;

    private float tempRange = 10f;

    [SerializeField]
    private LayerMask mask;

    void Start()
    {
        cam = GetComponent<CameraManager>().GetActiveCamera();
    }

    void Update()
    {
        Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward) * tempRange, Color.green);
    }

    [Client]
    public void Attack()
    {
        RaycastHit hit;
        cam = GetComponent<CameraManager>().GetActiveCamera();
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, tempRange, mask))
        {
            if(hit.collider.tag == "Player")
            {
                CmdPlayerHit(hit.collider.name, 10f);
            }
        }
    }

    [Command]
    private void CmdPlayerHit(string playerID, float damage)
    {
        Debug.Log(playerID + " hit once.");
        Player player = GameManager.GetPlayer(playerID);
        player.TakeDamage(damage);
    }
}
