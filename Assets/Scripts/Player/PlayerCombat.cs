﻿using UnityEngine;
using UnityEngine.Networking;

public class PlayerCombat : NetworkBehaviour
{
    private GameObject cam;
    private PlayerAnimation animate;

    private float tempRange = 10f;

    [SerializeField] private LayerMask mask;

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
        animate.Trigger("MainAttack");
        Attack();  
    }

    public void SpecialAttack()
    {
        animate.Trigger("SpecialAttack");
        Attack();
    }

    public void Block()
    {
        animate.SetBlock(true);
    }

    public void CounterAttack()
    {
        Attack();
    }

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
}
