using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [HideInInspector]
    public static Player playerComponent;

    public GameObject playerSpecificUI;
    public GameObject pauseMenu;
    public GameObject scoreboard;

    void Start()
    {
        PauseMenu.IsActive = false;
        CursorLock();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleUI(pauseMenu);
            PauseMenu.IsActive = pauseMenu.activeSelf;
            CursorLock();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleUI(scoreboard);
        }
    }

    public void SetPlayerComponent(Player playerComponent)
    {
        PlayerUI.playerComponent = playerComponent;
    }

    public Player GetPlayerComponent()
    {
        return playerComponent;
    }

    public void ToggleUI(GameObject elem)
    {
        elem.SetActive(!elem.activeSelf);
    }

    public static void CursorLock()
    {
        if (PauseMenu.IsActive)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
