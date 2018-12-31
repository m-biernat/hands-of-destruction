using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [HideInInspector]
    public static Player playerComponent;

    [SerializeField]
    private GameObject playerSpecificUI;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetPlayerComponent(Player playerComponent)
    {
        PlayerUI.playerComponent = playerComponent;
    }

    public Player GetPlayerComponent()
    {
        return playerComponent;
    }

    public void TogglePlayerSpecificUI()
    {
        if (playerSpecificUI.activeSelf)
            playerSpecificUI.SetActive(false);
        else
            playerSpecificUI.SetActive(true);
    }
}
