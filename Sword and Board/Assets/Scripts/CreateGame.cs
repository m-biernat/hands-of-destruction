using UnityEngine;
using UnityEngine.Networking;

public class CreateGame : MonoBehaviour
{
    private NetworkManager networkManager;

    private uint roomSize = 10;

    public string RoomName { get; set; }

    void Start()
    {
        networkManager = NetworkManager.singleton;
        if (!networkManager.matchMaker) networkManager.StartMatchMaker();
    }

    public void CreateRoom()
    {
        if(RoomName != null && RoomName != "")
        {
            networkManager.matchMaker.CreateMatch(RoomName, roomSize, true, "", "", "", 0, 0, networkManager.OnMatchCreate);
        }
    }
}
