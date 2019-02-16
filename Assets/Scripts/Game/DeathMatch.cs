using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class DeathMatch : NetworkBehaviour
{
    private GameManager gameManager;

	void Start ()
    {
		if (isServer)
        {
            gameManager = GameManager.instance;

            if (gameManager.settings.teamAssignEnabled)
                gameManager.onPlayerKilledCallback += UpdateTeamScore;
            else
                gameManager.onPlayerKilledCallback += UpdateTopScore;
        }
	}

    public void UpdateTeamScore(string playerID, string sourceID)
    {
        Player player = GameManager.GetPlayer(sourceID);

        if (player.teamID == 1)
            gameManager.teamRedPoints += gameManager.settings.pointsMultiplier;
        else
            gameManager.teamBluePoints += gameManager.settings.pointsMultiplier;
    }

    public void UpdateTopScore(string playerID, string sourceID)
    {
        List<Player> players = GameManager.GetAllPlayers();

        gameManager.teamRedPoints = (short)players[0].score;
    }
}
