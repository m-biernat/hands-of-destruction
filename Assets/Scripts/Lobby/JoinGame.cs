using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System.Collections.Generic;

public class JoinGame : MonoBehaviour
{
    private NetworkManager networkManager;

    private StatusText status;

    [SerializeField] private GameModeSelect selection;

    private string networkAddress = "localhost";

    private const string
        T_LOOKING = "Looking for game...",
        T_CON_FAIL = "Couldn't connect to matchmaking services.",
        T_NO_MATCH = "There are no games available.",
        T_SELECT = "Select game mode!",
        T_SELECT_D = "You have to select specific game mode!",
        T_JOINING = "Joining game...";

    void Start()
    {
        networkManager = NetworkManager.singleton;
        if (!networkManager.matchMaker) networkManager.StartMatchMaker();

        status = GetComponent<LobbyUI>().GetStatusText();
    }

    public void QuickJoin()
    {
        string selected = selection.GetValue();

        if(selected == selection.Label)
        {
            status.SetStatus(T_SELECT, false);
            return;
        }

        if (selected == selection.Func)
        {
            selection.SetRandom();
            selected = selection.GetValue();
        }

        networkManager.matchMaker.ListMatches(0, 10, selected, true, 0, 0, OnMatchList);
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
            networkManager.onlineScene = match.name.Substring(match.name.LastIndexOf('#') + 1);
            networkManager.matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, networkManager.OnMatchJoined);
            status.SetStatus(T_JOINING, false);
        }
        else
            status.SetStatus(T_NO_MATCH, false);
    }

    public void DirectJoin()
    {
        string selected = selection.GetValue();

        if (selected == selection.Label || selected == selection.Func)
        {
            status.SetStatus(T_SELECT_D, false);
        }
        else
        {
            networkManager.networkAddress = networkAddress;
            networkManager.StartClient();
            status.SetStatus(T_JOINING, false);
        }     
    }

    public void SetNetworkAddress(string networkAddress)
    { this.networkAddress = networkAddress; }
}
