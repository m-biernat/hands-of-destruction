using UnityEngine;
using UnityEngine.Networking;

public class MatchInfo : NetworkBehaviour
{
    [SyncVar] public byte teamRedSize = 0;
    [SyncVar] public byte teamBlueSize = 0;

    [SyncVar] public int teamRedPoints = 0;
    [SyncVar] public int teamBluePoints = 0;

    public MatchSettings settings;

    public static MatchInfo instance;

    void Awake()
    {
        if (instance)
            Debug.LogError("Too many instances of MatchInfo!");
        else
            instance = this;
    }

    public void IncrementTeamSize(byte teamID)
    {
        if (teamID == 1) teamRedSize++;
        if (teamID == 2) teamBlueSize++;
    }

    public void DecrementTeamSize(byte teamID)
    {
        if (teamID == 1) teamRedSize--;
        if (teamID == 2) teamBlueSize--;
    }
}
