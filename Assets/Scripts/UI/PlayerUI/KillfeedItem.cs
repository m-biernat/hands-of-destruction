using UnityEngine;
using UnityEngine.UI;

public class KillfeedItem : MonoBehaviour
{
    [SerializeField] private Text killerNameText;
    [SerializeField] private Text victimNameText;

    [SerializeField] private Color teamRedColor;
    [SerializeField] private Color teamBlueColor;

    [SerializeField] private Color highlightColor;

    public void SetItem(Player victim, Player killer)
    {
        victimNameText.text = victim.playerName;
        killerNameText.text = killer.playerName;

        if (victim.teamID == 1)
        {
            victimNameText.color = teamRedColor;
            killerNameText.color = teamBlueColor;
        }

        if (victim.teamID == 2)
        {
            victimNameText.color = teamBlueColor;
            killerNameText.color = teamRedColor;
        }

        if (victim.name == GameManager.clientID)
            victimNameText.color = highlightColor;

        if (killer.name == GameManager.clientID)
            killerNameText.color = highlightColor;
    }

}
