using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public static Player playerComponent;

    public GameObject playerSpecificUI;
    public GameObject pauseMenu;
    public GameObject scoreboard;

    public GameObject teamUI;
    public GameObject soloUI;

    public Text objectiveText;

    public EffectsUI effectsUI;

    private Canvas canvas;

    void Start()
    {
        PauseMenu.IsActive = false;
        CursorLock();

        objectiveText.text = GameManager.instance.settings.objectiveText;

        canvas = GetComponent<Canvas>();
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
            if (Input.GetButtonDown("Show Scoreboard"))
            { scoreboard.SetActive(true); }
            else if (Input.GetButtonUp("Show Scoreboard"))
            { scoreboard.SetActive(false); }
        }

        if (Input.GetButtonDown("Hide UI") 
            && GameManager.instance.eventCode == 0)
        {
            canvas.enabled = !canvas.enabled;
        }
        if(GameManager.instance.eventCode != 0 && !canvas.enabled)
        { canvas.enabled = true; }
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
