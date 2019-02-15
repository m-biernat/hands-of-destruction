using UnityEngine;
using UnityEngine.Networking;

public class GameInfo : NetworkBehaviour
{
    public static GameInfo instance;

    [SyncVar] public byte teamRedSize = 0;
    [SyncVar] public byte teamBlueSize = 0;

    [SyncVar] public byte playersCount = 0;

    [SyncVar] public short teamRedPoints = 0;
    [SyncVar] public short teamBluePoints = 0;

    [SyncVar] public short timeCounter = 0;

    public GameSettings settings;

    public delegate void OnPlayerKilledCallback(string playerID, string sourceID);
    public OnPlayerKilledCallback onPlayerKilledCallback;

    void Awake()
    {
        if (instance)
            Debug.LogError("Too many instances of GameInfo!");
        else
            instance = this;

        // I have to move it to StageManager (later)
        teamRedPoints = settings.pointsAtStart;
        teamBluePoints = settings.pointsAtStart;
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
