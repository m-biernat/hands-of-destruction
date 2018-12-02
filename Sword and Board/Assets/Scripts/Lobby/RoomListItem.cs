using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking.Match;

public class RoomListItem : MonoBehaviour
{
    private MatchInfoSnapshot match;

    [SerializeField] private Text roomNameText;
    [SerializeField] private Text gameModeText;
    [SerializeField] private Text roomSizeText;

    private string roomNamePart, gameModePart;

    private const string T_DEFAULT = "NOT_LISTED";

    public delegate void JoinRoomDelegate(MatchInfoSnapshot _match);
    private JoinRoomDelegate joinRoomCallback;

    public void Setup(MatchInfoSnapshot _match, JoinRoomDelegate _joinRoomCallback)
    {
        match = _match;
        joinRoomCallback = _joinRoomCallback;

        SplitMatchName(match.name);

        roomNameText.text = roomNamePart;
        gameModeText.text = gameModePart;
        roomSizeText.text = match.currentSize + " / " + match.maxSize;
    }

    private void SplitMatchName(string name)
    {
        int ind = name.LastIndexOf('#');
        if(ind > 0)
        {
            roomNamePart = name.Substring(0, ind);
            gameModePart = name.Substring(ind + 1);
        }
        else
        {
            roomNamePart = name;
            gameModePart = T_DEFAULT;
        }
        
    }

    public void JoinRoom()
    {
        joinRoomCallback.Invoke(match);
    }
}
