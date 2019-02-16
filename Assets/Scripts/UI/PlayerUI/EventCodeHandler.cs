using UnityEngine;

public class EventCodeHandler : MonoBehaviour
{
    private GameManager gameManager;
    
    [SerializeField] private GameObject scoreboard;
    [SerializeField] private GameObject countdown;
    [SerializeField] private GameObject waitingMessage;
    [SerializeField] private GameObject resultMessage;
	
    void Start()
    {
        gameManager = GameManager.instance;
    }

	void FixedUpdate ()
    {
        CheckEvent();
	}

    private void CheckEvent()
    {
        switch (gameManager.eventCode)
        {
            case 0:
                break;
            case 1:
                SwitchElement(waitingMessage, true);
                break;
            case 2:
                SwitchElement(waitingMessage, false);
                break;
            case 3:
                SwitchElement(scoreboard, true);
                break;
            case 4:
                SwitchElement(scoreboard, false);
                break;
            case 5:
                SwitchElement(countdown, true);
                break;
            case 6:
                SwitchElement(resultMessage, true);
                break;
            case 7:
                SwitchElement(resultMessage, false);
                break;
            default:
                Debug.LogError("Unknown EventCode!");
                break;
        }
    }

    private void SwitchElement(GameObject go, bool active)
    {
        if (!(go.activeSelf == active))
            go.SetActive(active);
    }
}
