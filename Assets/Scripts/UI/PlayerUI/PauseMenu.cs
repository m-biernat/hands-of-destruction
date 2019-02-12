using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class PauseMenu : MonoBehaviour
{
    public static bool IsActive = false;

    private NetworkManager networkManager;

    [SerializeField]
    private GameObject menuView, settingsView;

	void Start()
    {
        menuView.SetActive(true);
        networkManager = NetworkManager.singleton;
	}

    public void Resume()
    {   
        if (settingsView.activeSelf)
        {
            settingsView.SetActive(false);
            menuView.SetActive(true);
            return;
        }
        gameObject.SetActive(false);
        IsActive = false;
        PlayerUI.CursorLock();
    }

    public void Settings()
    {
        menuView.SetActive(false);
        settingsView.SetActive(true);
    }

    public void Disconnect()
    {
        MatchInfo matchInfo = networkManager.matchInfo;
        if (matchInfo != null)
        {
            networkManager.matchMaker.DropConnection(matchInfo.networkId,
                   matchInfo.nodeId, 0, networkManager.OnDropConnection);
        }
        networkManager.StopHost();
    }
}
