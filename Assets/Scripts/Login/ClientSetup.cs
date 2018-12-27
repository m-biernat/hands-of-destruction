using UnityEngine;
using UnityEngine.UI;

public class ClientSetup : MonoBehaviour
{
    [SerializeField]
    private InputField playerNameInput;

    [SerializeField]
    private GameObject nextView;

    private StatusText status;
    private LobbyUI lobbyUI;

    private const string
    T_EMPTY = "You have to enter a name!",
    T_SHORT = "Name is too short!",
    T_LONG = "Name is too long!",
    NAME_PREFIX = "Player ";

    private bool changed = false;

    void Start()
    {
        lobbyUI = GetComponent<LobbyUI>();
        status = lobbyUI.GetStatusText();

        ClientSettings.Load();
        playerNameInput.text = ClientSettings.playerName;
    }

    public void SetPlayerName()
    {
        string playerName = playerNameInput.text;

        if (playerName != ClientSettings.playerName)
        {
            changed = true;
        }

        if(playerName == null || playerName == "")
        {
            status.SetStatus(T_EMPTY, false);
            return;
        }

        if(playerName.Length < 2)
        {
            status.SetStatus(T_SHORT, false);
            return;
        }

        if (playerName.Length > 16)
        {
            status.SetStatus(T_LONG, false);
            return;
        }

        status.ClearStatus();
        ClientSettings.playerName = playerName;
        if (changed) ClientSettings.SaveChanges();
        lobbyUI.ChangeView(nextView);
    }

    public void SetRandomName()
    {
        string hash = Random.Range(1000, 9999).ToString();

        playerNameInput.text = NAME_PREFIX + '#' + hash;
    }

    public void SetSelectedMagicID(int magicID)
    {
        ClientSettings.selectedMagicID = (ushort)magicID;
    }

    public void SetSelectedArmorID(int armorID)
    {
        ClientSettings.selectedArmorID = (ushort)armorID;
    }

    public void SetRandomMagicID()
    {
        ClientSettings.selectedMagicID = (ushort)Random.Range(1, 1);
    }

    public void SetRandomArmorID()
    {
        ClientSettings.selectedArmorID = (ushort)Random.Range(1, 3);
    }
}
