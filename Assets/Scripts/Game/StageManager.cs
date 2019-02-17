using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System.Collections;

public class StageManager : NetworkBehaviour
{
    private GameManager gameManager;

    private NetworkManager networkManager;

    void Start ()
    {
		if(isServer)
        {
            gameManager = GameManager.instance;
            networkManager = NetworkManager.singleton;

            StartCoroutine(WaitForPlayers());
        }
	}

    private IEnumerator WaitForPlayers()
    {
        gameManager.lockControll = true;
        gameManager.eventCode = 1; // ShowWaitingMessage

        gameManager.timeCounter = gameManager.settings.prepareTime;
        while (gameManager.timeCounter > 0)
        {
            gameManager.timeCounter--;
            yield return new WaitForSeconds(1f);

            if (gameManager.playersCount == HostGame.roomSize)
                gameManager.timeCounter = 0;
        }

        MatchInfo matchInfo = networkManager.matchInfo;
        if (matchInfo != null)
        {
            networkManager.matchMaker.SetMatchAttributes(matchInfo.networkId, false, 0, networkManager.OnDestroyMatch);
        }

        gameManager.eventCode = 2; // HideWaitingMessage
        yield return new WaitForSeconds(.5f);

        gameManager.eventCode = 3; // ShowScoreboard
        yield return new WaitForSeconds(5f);

        gameManager.eventCode = 4; // HideScoreboard
        yield return new WaitForSeconds(.5f);

        gameManager.eventCode = 5; // StartCountdown
        yield return new WaitForSeconds(.5f);
        gameManager.eventCode = 0;
        yield return new WaitForSeconds(3f);

        if (gameManager.playersCount == 1)
            StartCoroutine(CompleteGame());
        else
            StartCoroutine(MaintainGameplay());
    }

    private IEnumerator MaintainGameplay()
    {
        gameManager.lockControll = false;

        gameManager.timeCounter = gameManager.settings.gameplayTime;
        while (gameManager.timeCounter > 0)
        {
            if (CheckGoal()) break;
            gameManager.timeCounter--;
            yield return new WaitForSeconds(1f);
        }

        StartCoroutine(CompleteGame());
    }

    private IEnumerator CompleteGame()
    {
        gameManager.timeCounter = 0;
        gameManager.lockControll = true;

        yield return new WaitForSeconds(1f);

        gameManager.eventCode = 6; // ShowResultMessage
        yield return new WaitForSeconds(5f);

        gameManager.eventCode = 7; // HideResultMessage
        yield return new WaitForSeconds(.5f);

        gameManager.eventCode = 3; // ShowScoreboard

        gameManager.timeCounter = gameManager.settings.completeTime;
        while (gameManager.timeCounter > 0)
        {
            gameManager.timeCounter--;
            yield return new WaitForSeconds(1f);
        }

        MatchInfo matchInfo = networkManager.matchInfo;
        if (matchInfo != null)
        {
            networkManager.matchMaker.DropConnection(matchInfo.networkId,
                   matchInfo.nodeId, 0, networkManager.OnDropConnection);
        }
        networkManager.StopHost();
    }

    private bool CheckGoal()
    {
        if (gameManager.teamRedPoints >= gameManager.settings.pointsToComplete ||
            gameManager.teamBluePoints >= gameManager.settings.pointsToComplete)
            return true;
        else
            return false;
    }
}
