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

    private Camera sceneCamera;

    void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
        }
        else
        {
            sceneCamera = Camera.main;
            if (sceneCamera) sceneCamera.gameObject.SetActive(false);

            playerUIInstance = Instantiate(playerUIPrefab);
            playerUIInstance.name = playerUIPrefab.name;

            PlayerUI playerUI = playerUIInstance.GetComponent<PlayerUI>();
            playerUI.SetPlayerComponent(GetComponent<Player>());

            CmdSetPlayer(transform.name, ClientSettings.playerName,
            ClientSettings.selectedMagicID, ClientSettings.selectedArmorID);

            CmdTeamAssign(transform.name);
        }

        GetComponent<Player>().Setup();
        // Cursor.lockState = CursorLockMode.Locked;
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
        Destroy(playerUIInstance);
        if (sceneCamera) sceneCamera.gameObject.SetActive(true);

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

        byte teamRedSize = MatchInfo.instance.teamRedSize;
        byte teamBlueSize = MatchInfo.instance.teamBlueSize;

        if (teamRedSize == teamBlueSize)
        {
            int rand = Random.Range(1, 3);
            player.teamID = (byte)rand;
            MatchInfo.instance.IncrementTeamSize((byte)rand);
        }

        else if (teamRedSize > teamBlueSize)
        {
            player.teamID = 2;
            MatchInfo.instance.IncrementTeamSize(2);
        }

        else if (teamBlueSize > teamRedSize)
        {
            player.teamID = 1;
            MatchInfo.instance.IncrementTeamSize(1);
        }
    }

    [Server]
    private void SrvTeamUnassing(string playerID)
    {
        Player player = GameManager.GetPlayer(playerID);

        MatchInfo.instance.DecrementTeamSize(player.teamID);
    }
}
