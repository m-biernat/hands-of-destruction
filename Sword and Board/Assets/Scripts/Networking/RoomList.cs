using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System.Collections.Generic;

public class RoomList : MonoBehaviour
{
    private NetworkManager networkManager;

    private StatusText status;

    private List<GameObject> roomList = new List<GameObject>();

    [SerializeField] private GameObject roomListItemPrefab;
    [SerializeField] private Transform roomListParent;

    private const string
        T_LOADING = "Loading...",
        T_LIST_FAIL = "Couldn't get room list",
        T_LIST_EMPTY = "There are no rooms available.",
        T_JOINING = "Joining room...";

    void Start()
    {
        networkManager = NetworkManager.singleton;
        if (!networkManager.matchMaker) networkManager.StartMatchMaker();

        status = GetComponent<Lobby>().GetStatusText();
    }

    public void RefreshRoomList()
    {
        networkManager.matchMaker.ListMatches(0, 20, "", true, 0, 0, OnMatchList);
        status.SetStatus(T_LOADING, false);
    }

    public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
    {
        if (matches == null || success == false)
        {
            status.SetStatus(T_LIST_FAIL, true);
            return;
        }

        ClearRoomList();

        foreach(var match in matches)
        {
            GameObject roomListItemGO = Instantiate(roomListItemPrefab);
            roomListItemGO.transform.SetParent(roomListParent);
            roomListItemGO.transform.localScale = new Vector3(1, 1, 1);

            RoomListItem roomListItem = roomListItemGO.GetComponent<RoomListItem>();
            if (roomListItem) roomListItem.Setup(match, JoinRoom);

            roomList.Add(roomListItemGO);
        }

        if (roomList.Count == 0)
            status.SetStatus(T_LIST_EMPTY, false);
        else
            status.ClearStatus();
    }

    private void ClearRoomList()
    {
        foreach (var item in roomList) Destroy(item);
        roomList.Clear();
    }

    public void JoinRoom(MatchInfoSnapshot match)
    {
        networkManager.matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, networkManager.OnMatchJoined);
        ClearRoomList();
        status.SetStatus(T_JOINING, false);
    }
}
