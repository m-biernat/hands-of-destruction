using UnityEngine;

public class TeamManager : MonoBehaviour
{
    public static void AssignPlayer(string playerID)
    {
        Player player = GameManager.GetPlayer(playerID);

        if (!GameManager.instance.settings.teamAssignEnabled)
        {
            player.teamID = 0;
            GameManager.instance.IncrementTeamSize(0);

            return;
        }

        byte teamRedSize = GameManager.instance.teamRedSize;
        byte teamBlueSize = GameManager.instance.teamBlueSize;

        if (teamRedSize == teamBlueSize)
        {
            int rand = Random.Range(1, 3);
            player.teamID = (byte)rand;
            GameManager.instance.IncrementTeamSize((byte)rand);
        }

        else if (teamRedSize > teamBlueSize)
        {
            player.teamID = 2;
            GameManager.instance.IncrementTeamSize(2);
        }

        else if (teamBlueSize > teamRedSize)
        {
            player.teamID = 1;
            GameManager.instance.IncrementTeamSize(1);
        }
    }

    public static void UnassingPlayer(string playerID)
    {
        Player player = GameManager.GetPlayer(playerID);
        GameManager.instance.DecrementTeamSize(player.teamID);
    }
}
