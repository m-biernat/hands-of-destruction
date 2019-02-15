using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static Camera sceneCamera;

    public static string clientID;

    private const string PLAYER_ID_PREFIX = "Player_";

    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

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

    public static void SetSceneCameraActive(bool isActive)
    {
        if (sceneCamera) sceneCamera.gameObject.SetActive(isActive);
    }
}
