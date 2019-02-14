using UnityEngine;
using System.Collections.Generic;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private Transform playerList;

    [SerializeField] private GameObject playerListItem;

    void OnEnable()
    {
        List<Player> players = GameManager.GetAllPlayers();

        foreach (Player player in players)
        {
            GameObject itemGO = Instantiate(playerListItem, playerList);
            ScoreboardItem item = itemGO.GetComponent<ScoreboardItem>();
            if (item) item.SetItem(player);
        }
    }

    void OnDisable()
    {
        foreach (Transform child in playerList)
        {
            Destroy(child.gameObject);
        }
    }
}
