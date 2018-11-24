using UnityEngine;
using UnityEngine.Networking;

public class HostGame : MonoBehaviour
{
    private NetworkManager networkManager;

    private string roomName = "";
    private uint roomSize = 10;

    void Start()
    {
        networkManager = NetworkManager.singleton;
        if (!networkManager.matchMaker) networkManager.StartMatchMaker();
    }

    public void CreateMatch()
    {
        if (roomName != null && roomName != "")
        {
            networkManager.matchMaker.CreateMatch(roomName, roomSize, true, "", "", "", 0, 0, networkManager.OnMatchCreate);
        }
    }


    public void CreateLocal()
    {
        networkManager.StartHost(networkManager.connectionConfig, (int)roomSize);
    }

    public void SetRoomName(string name) { roomName = name; }  
}
