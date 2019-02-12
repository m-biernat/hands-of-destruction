using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    private Behaviour[] componentsToDisable;

    [SerializeField]
    private string remoteLayerName = "RemotePlayer";

    [SerializeField]
    private GameObject playerUIPrefab;
    private GameObject playerUIInstance;

    void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
        }
        else
        {
            GameManager.sceneCamera = Camera.main;

            playerUIInstance = Instantiate(playerUIPrefab);
            playerUIInstance.name = playerUIPrefab.name;

            Player player = GetComponent<Player>();
            player.playerUI = playerUIInstance.GetComponent<PlayerUI>();
            player.playerUI.SetPlayerComponent(player);

            CmdSetPlayer(transform.name, ClientSettings.playerName,
            ClientSettings.selectedMagicID, ClientSettings.selectedArmorID);

            CmdTeamAssign(transform.name);
        }
        
        StartCoroutine(GetComponent<Player>().Setup());
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        string netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player player = GetComponent<Player>();

        GameManager.RegisterPlayer(netID, player);
    }

    void OnDisable()
    {
        if(isLocalPlayer)
        {
            Destroy(playerUIInstance);
            GameManager.SetSceneCameraActive(true);
        } 

        SrvTeamUnassing(transform.name);
        GameManager.DeregisterPlayer(transform.name);   
    }

    private void DisableComponents()
    {
        foreach (var comp in componentsToDisable) comp.enabled = false;
    }

    private void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    [Command]
    private void CmdSetPlayer(string playerID, string name, byte magicID, byte armorID)
    {
        Player player = GameManager.GetPlayer(playerID);

        if(player)
        {
            player.playerName = name;
            player.magicID = magicID;
            player.armorID = armorID;
        }
    }

    [Command]
    private void CmdTeamAssign(string playerID)
    {
        Player player = GameManager.GetPlayer(playerID);

        byte teamRedSize = GameInfo.instance.teamRedSize;
        byte teamBlueSize = GameInfo.instance.teamBlueSize;

        if (teamRedSize == teamBlueSize)
        {
            int rand = Random.Range(1, 3);
            player.teamID = (byte)rand;
            GameInfo.instance.IncrementTeamSize((byte)rand);
        }

        else if (teamRedSize > teamBlueSize)
        {
            player.teamID = 2;
            GameInfo.instance.IncrementTeamSize(2);
        }

        else if (teamBlueSize > teamRedSize)
        {
            player.teamID = 1;
            GameInfo.instance.IncrementTeamSize(1);
        }
    }

    [Server]
    private void SrvTeamUnassing(string playerID)
    {
        Player player = GameManager.GetPlayer(playerID);

        GameInfo.instance.DecrementTeamSize(player.teamID);
    }
}
