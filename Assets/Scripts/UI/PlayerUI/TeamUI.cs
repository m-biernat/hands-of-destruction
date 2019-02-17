using UnityEngine;
using UnityEngine.UI;

public class TeamUI : MonoBehaviour
{
    [SerializeField] private Text teamRedScoreText;
    [SerializeField] private Text teamBlueScoreText;

    [SerializeField] private Text playerCountText;

    [SerializeField] private Image teamRedIndicator;
    [SerializeField] private Image teamBlueIndicator;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.instance;
    }

	void LateUpdate ()
    {
        SetScoreText();
        SetPlayerCountText();
	}

    private void SetScoreText()
    {
        teamRedScoreText.text = gameManager.teamRedPoints.ToString();
        teamBlueScoreText.text = gameManager.teamBluePoints.ToString();
    }

    private void SetPlayerCountText()
    {
        string teamRedSize = gameManager.teamRedSize.ToString();
        string teamBlueSize = gameManager.teamBlueSize.ToString();

        playerCountText.text = teamRedSize + " vs " + teamBlueSize;
    }

    public void SetTeamIndicator(short teamID)
    {
        if (teamID == 1) teamRedIndicator.enabled = true;
        if (teamID == 2) teamBlueIndicator.enabled = true;
    }
}
