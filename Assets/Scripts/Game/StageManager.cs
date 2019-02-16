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
        // Show scoreboard RpcShowScoreboard()

        gameManager.timeCounter = 10;
        while (gameManager.timeCounter > 0)
        {
            gameManager.timeCounter--;
            yield return new WaitForSeconds(1f);
        }

        // Close scoreboard RpcHideScoreboard()

        // Show countdown ui RpcShowCountdown()
        yield return new WaitForSeconds(3f);

        StartCoroutine(MaintainGameplay());
    }

    private IEnumerator MaintainGameplay()
    {
        gameManager.lockControll = false;

        gameManager.timeCounter = gameManager.settings.gameplayTime;
        while (gameManager.timeCounter > 0)
        {
            if (CheckGoal()) StartCoroutine(CompleteGame());
            gameManager.timeCounter--;
            yield return new WaitForSeconds(1f);
        }

        StartCoroutine(CompleteGame());
    }

    private IEnumerator CompleteGame()
    {
        gameManager.lockControll = true;
        // Show winner RpcShowWinner();
        yield return new WaitForSeconds(10f);
        // Show scoreboard RpcShowScoreboard()

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
