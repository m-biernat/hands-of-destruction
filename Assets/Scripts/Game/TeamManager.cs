using UnityEngine;

public class TeamManager : MonoBehaviour
{
    public static void AssignPlayer(string playerID)
    {
        Player player = GameManager.GetPlayer(playerID);

        if (!GameInfo.instance.settings.teamAssignEnabled)
        {
            player.teamID = 0;
            GameInfo.instance.IncrementTeamSize(0);

            return;
        }

        byte teamRedSize = GameInfo.instance.teamRedSize;
        byte teamBlueSize = GameInfo.instance.teamBlueSize;

        if (teamRedSize == teamBlueSize)
        {
            int rand = Random.Range(1, 3);
            player.teamID = (byte)rand;
            GameInfo.instance.IncrementTeamSize((byte)rand);
        }

        else if (teamRedSize > teamBlueSize)
        {
            player.teamID = 2;
            GameInfo.instance.IncrementTeamSize(2);
        }

        else if (teamBlueSize > teamRedSize)
        {
            player.teamID = 1;
            GameInfo.instance.IncrementTeamSize(1);
        }
    }

    public static void UnassingPlayer(string playerID)
    {
        Player player = GameManager.GetPlayer(playerID);
        GameInfo.instance.DecrementTeamSize(player.teamID);
    }
}
