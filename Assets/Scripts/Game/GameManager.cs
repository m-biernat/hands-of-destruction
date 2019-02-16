using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class GameManager : NetworkBehaviour
{
    public static GameManager instance;

    public static Camera sceneCamera;

    public static string clientID;

    private const string PLAYER_ID_PREFIX = "Player_";

    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    [SyncVar] public byte teamRedSize = 0;
    [SyncVar] public byte teamBlueSize = 0;

    [SyncVar] public byte playersCount = 0;

    [SyncVar] public short teamRedPoints = 0;
    [SyncVar] public short teamBluePoints = 0;

    [SyncVar] public short timeCounter = 0;

    public GameSettings settings;

    public delegate void OnPlayerKilledCallback(string playerID, string sourceID);
    public OnPlayerKilledCallback onPlayerKilledCallback;

    [SyncVar] public bool lockControll = false;

    void Awake()
    {
        if (instance)
            Debug.LogError("Too many instances of GameManager!");
        else
            instance = this;
    }

    public static void RegisterPlayer (string netID, Player player)
    {
        string playerID = PLAYER_ID_PREFIX + netID;
        players.Add(playerID, player);
        player.transform.name = playerID;
    }
	
    public static void DeregisterPlayer(string playerID)
    {
        players.Remove(playerID);
    }

    public static Player GetPlayer(string playerID)
    {
        return players[playerID];
    }

    public static List<Player> GetAllPlayers()
    {
        var list = new List<Player>(players.Values);
        list.Sort((x, y) => y.score.CompareTo(x.score));

        return list;
    }

    public void IncrementTeamSize(byte teamID)
    {
        if (teamID == 1) teamRedSize++;
        if (teamID == 2) teamBlueSize++;
        playersCount++;
    }

    public void DecrementTeamSize(byte teamID)
    {
        if (teamID == 1) teamRedSize--;
        if (teamID == 2) teamBlueSize--;
        playersCount--;
    }

    public static void SetSceneCameraActive(bool isActive)
    {
        if (sceneCamera) sceneCamera.gameObject.SetActive(isActive);
    }
}
