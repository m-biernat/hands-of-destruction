using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TeamUI : MonoBehaviour
{
    [SerializeField] private Text teamRedScoreText;
    [SerializeField] private Text teamBlueScoreText;

    [SerializeField] private Text playerCountText;

    [SerializeField] private Image teamRedIndicator;
    [SerializeField] private Image teamBlueIndicator;

    private GameManager gameManager;

    void OnEnable()
    {
        gameManager = GameManager.instance;

        StartCoroutine(SetTeamIndicator());
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

    private IEnumerator SetTeamIndicator()
    {
        yield return new WaitForSeconds(1f);

        short teamID = GameManager.GetPlayer(GameManager.clientID).teamID;

        if (teamID == 1) teamRedIndicator.enabled = true;
        if (teamID == 2) teamBlueIndicator.enabled = true;
    }
}
