using UnityEngine;
using UnityEngine.Networking;

public class HostGame : MonoBehaviour
{
    private NetworkManager networkManager;

    private string roomName = "";
    private uint roomSize = 10;

    private StatusText status;

    [SerializeField] private GameModeSelect selection;

    private const string
        T_NAME = "Enter room name!",
        T_SELECT = "Select game mode!",
        T_CREATE = "Creating game...";

    void Start()
    {
        networkManager = NetworkManager.singleton;
        if (!networkManager.matchMaker) networkManager.StartMatchMaker();

        status = GetComponent<LobbyUI>().GetStatusText();
    }

    public void CreateMatch()
    {
        string selected = selection.GetValue();

        if (roomName == null || roomName == "")
        {
            status.SetStatus(T_NAME, false);
            return;
        }
        if (selected == selection.Label)
        {
            status.SetStatus(T_SELECT, false);
            return;
        }

        if (selected == selection.Func)
        {
            selection.SetRandom();
            selected = selection.GetValue();
        }

        //networkManager.ServerChangeScene(selected);
        networkManager.onlineScene = selected;
        roomName += "#" + selected;

        networkManager.matchMaker.CreateMatch(roomName, roomSize, true, "", "", "", 0, 0, networkManager.OnMatchCreate);
        status.SetStatus(T_CREATE, false);
    }

    public void CreateLocal()
    {
        string selected = selection.GetValue();

        if (selected == selection.Label)
        {
            status.SetStatus(T_SELECT, false);
            return;
        }

        if (selected == selection.Func) selection.SetRandom();

        networkManager.networkAddress = "localhost";
        //networkManager.ServerChangeScene(selected);

        networkManager.StartHost();
        status.SetStatus(T_CREATE, false);
    }

    public void SetRoomName(string roomName)
    { this.roomName = roomName; }

    public void SetRoomSize(float roomSize)
    { this.roomSize = (uint)roomSize; }
}
