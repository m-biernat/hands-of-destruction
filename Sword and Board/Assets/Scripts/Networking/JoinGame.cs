using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System.Collections.Generic;

public class JoinGame : MonoBehaviour
{
    private NetworkManager networkManager;

    private StatusText status;

    private string networkAddress = "localhost";

    private const string
        T_LOOKING = "Looking for game...",
        T_CON_FAIL = "Couldn't connect to matchmaking services.",
        T_NO_MATCH = "There are no games available.",
        T_JOINING = "Joining game...";

    void Start()
    {
        networkManager = NetworkManager.singleton;
        if (!networkManager.matchMaker) networkManager.StartMatchMaker();

        status = GetComponent<LobbyUI>().GetStatusText();
    }

    public void QuickJoin()
    {
        networkManager.matchMaker.ListMatches(0, 10, "", true, 0, 0, OnMatchList);
        status.SetStatus(T_LOOKING, false);
    }

    public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
    {
        if (matches == null || success == false)
        {
            status.SetStatus(T_CON_FAIL, true);
            return;
        }

        if (matches.Count > 0)
        {
            var match = matches[0];
            networkManager.matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, networkManager.OnMatchJoined);
            status.SetStatus(T_JOINING, false);
        }
        else
            status.SetStatus(T_NO_MATCH, false);
    }

    public void DirectJoin()
    {
        networkManager.networkAddress = networkAddress;
        networkManager.StartClient();
    }

    public void SetNetworkAddress(string networkAddress)
    { this.networkAddress = networkAddress; }
}
