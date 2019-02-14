using UnityEngine;
using UnityEngine.UI;

public class ScoreboardItem : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Text killsText;
    [SerializeField] private Text deathsText;
    [SerializeField] private Text scoreText;

    [SerializeField] private Image itemBackgroundImage;

    [SerializeField] private Color teamRedColor;
    [SerializeField] private Color teamBlueColor;

    [SerializeField] private Color highlightColor;

    public void SetItem(Player player)
    {
        nameText.text = player.playerName;
        killsText.text = player.kills.ToString();
        deathsText.text = player.deaths.ToString();
        scoreText.text = player.score.ToString();

        if (player.teamID == 1)
            itemBackgroundImage.color = teamRedColor;
        if (player.teamID == 2)
            itemBackgroundImage.color = teamBlueColor;

        if (player.name == GameManager.clientID)
        {
            nameText.color = highlightColor;
            killsText.color = highlightColor;
            deathsText.color = highlightColor;
            scoreText.color = highlightColor;
        }
    }
}
