using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System.Collections;
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
        T_JOINING = "Joining game...",
        T_FAILED = "Failed to connect!";

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
            selected = "";
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
            StartCoroutine(WaitForJoin());
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
            networkManager.onlineScene = selection.GetSceneName(selected);
            networkManager.networkAddress = networkAddress;
            networkManager.StartClient();
            StartCoroutine(WaitForJoin());
        }     
    }

    IEnumerator WaitForJoin()
    {
        status.SetStatus(T_JOINING, false);
        yield return new WaitForSeconds(10);

        status.SetStatus(T_FAILED, true);
        yield return new WaitForSeconds(2);

        status.ClearStatus();

        MatchInfo matchInfo = networkManager.matchInfo;
        if (matchInfo != null)
        {
            networkManager.matchMaker.DropConnection(matchInfo.networkId,
                   matchInfo.nodeId, 0, networkManager.OnDropConnection);
        }
        networkManager.StopHost();

        if (!networkManager.matchMaker) networkManager.StartMatchMaker();
    }

    public void SetNetworkAddress(string networkAddress)
    { this.networkAddress = networkAddress; }
}
