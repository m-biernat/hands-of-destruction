using UnityEngine;

public class LobbyUI : MonoBehaviour {

    private static GameObject currentView;

    [SerializeField] private GameObject defaultView;

    [SerializeField] private StatusText status;

    void Start()
    {
        currentView = defaultView;
        currentView.SetActive(true);
    }

    public void ChangeView(GameObject view)
    {
        currentView.SetActive(false);
        status.ClearStatus();
        currentView = view;
        currentView.SetActive(true);
    }

    public StatusText GetStatusText()
    {
        return status;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
