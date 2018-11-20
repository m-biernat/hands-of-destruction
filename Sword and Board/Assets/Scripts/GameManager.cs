using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    //public static GameManager instance;

    //void Awake()
    //{
    //    if (instance) instance = this;
    //    else Debug.LogError("Too many instances of GameManager");
    //}

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
}
