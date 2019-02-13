using UnityEngine;
using UnityEngine.Networking;

public class GameInfo : NetworkBehaviour
{
    [SyncVar] public byte teamRedSize = 0;
    [SyncVar] public byte teamBlueSize = 0;

    [SyncVar] public byte playersCount = 0;

    [SyncVar] public int teamRedPoints = 0;
    [SyncVar] public int teamBluePoints = 0;

    public GameSettings settings;

    public static GameInfo instance;

    void Awake()
    {
        if (instance)
            Debug.LogError("Too many instances of GameInfo!");
        else
            instance = this;
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
}
