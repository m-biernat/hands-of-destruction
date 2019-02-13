using UnityEngine;
using UnityEngine.UI;

public class TeamUI : MonoBehaviour
{
    [SerializeField] private Text teamRedScoreText;
    [SerializeField] private Text teamBlueScoreText;

    [SerializeField] private Text playerCountText;

    private GameInfo gameInfo;

    void Start()
    {
        gameInfo = GameInfo.instance;
    }

	void LateUpdate ()
    {
        SetScoreText();
        SetPlayerCountText();
	}

    private void SetScoreText()
    {
        teamRedScoreText.text = gameInfo.teamRedPoints.ToString();
        teamBlueScoreText.text = gameInfo.teamBluePoints.ToString();
    }

    private void SetPlayerCountText()
    {
        string teamRedSize = gameInfo.teamRedSize.ToString();
        string teamBlueSize = gameInfo.teamBlueSize.ToString();

        playerCountText.text = teamRedSize + " vs " + teamBlueSize;
    }
}
