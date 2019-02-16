using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField] private Text resultText;

    [SerializeField] private Color tieColor;
    [SerializeField] private Color victoryColor;
    [SerializeField] private Color defeatColor;

    private GameManager gameManager;

    private const string T_TIE = "TIE!", T_VICTORY = "VICTORY!", T_DEFEAT = "DEFEAT!";

    void OnEnable()
    {
        gameManager = GameManager.instance;
        ShowResult();
    }

    private void ShowResult()
    {
        Player player = GameManager.GetPlayer(GameManager.clientID);

        if (!gameManager.settings.teamAssignEnabled)
        {
            if (gameManager.teamRedPoints == player.score)
                SetResultText(T_VICTORY, victoryColor);
            else
                SetResultText(T_DEFEAT, defeatColor);
            return;
        }

        if (gameManager.teamRedPoints == gameManager.teamBluePoints)
        {
            SetResultText(T_TIE, tieColor);
            return;
        }

        if (gameManager.teamRedPoints > gameManager.teamBluePoints)
        {
            if (player.teamID == 1)
                SetResultText(T_VICTORY, victoryColor);
            else
                SetResultText(T_DEFEAT, defeatColor);
        }
        else
        {
            if (player.teamID == 2)
                SetResultText(T_VICTORY, victoryColor);
            else
                SetResultText(T_DEFEAT, defeatColor);
        }
    }

    private void SetResultText(string text, Color color)
    {
        resultText.text = text;
        resultText.color = color;
    }
}
