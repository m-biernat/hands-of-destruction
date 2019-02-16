﻿using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public static Player playerComponent;

    public GameObject playerSpecificUI;
    public GameObject pauseMenu;
    public GameObject scoreboard;
    public Text objectiveText;

    void Start()
    {
        PauseMenu.IsActive = false;
        CursorLock();

        objectiveText.text = GameManager.instance.settings.objectiveText;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleUI(pauseMenu);
            PauseMenu.IsActive = pauseMenu.activeSelf;
            CursorLock();
        }

        if (GameManager.instance.eventCode != 3)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            { scoreboard.SetActive(true); }
            else if (Input.GetKeyUp(KeyCode.Tab))
            { scoreboard.SetActive(false); }
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
