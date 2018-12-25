using UnityEngine;
using UnityEngine.UI;

public class DisplayName : MonoBehaviour
{
    [SerializeField] private Text playerNameText;

	void Start ()
    {
        playerNameText.text = ClientSettings.playerName;
	}
}
