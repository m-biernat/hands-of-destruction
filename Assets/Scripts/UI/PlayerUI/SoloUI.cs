using UnityEngine;
using UnityEngine.UI;

public class SoloUI : MonoBehaviour
{

    [SerializeField] private Text playerScoreText;
    [SerializeField] private Text topScoreText;

    [SerializeField] private Text playerCountText;

    private GameManager gameManager;

    private Player player;

    void Start()
    {
        gameManager = GameManager.instance;

        player = GameManager.GetPlayer(GameManager.clientID);
    }

    void LateUpdate()
    {
        SetScoreText();
        SetPlayerCountText();
    }

    private void SetScoreText()
    {
        playerScoreText.text = player.score.ToString();
        topScoreText.text = gameManager.teamRedPoints.ToString();
    }

    private void SetPlayerCountText()
    {
        string enemiesCount = (gameManager.playersCount - 1).ToString();

        playerCountText.text = "1 vs " + enemiesCount;
    }
}
