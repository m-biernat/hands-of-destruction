using UnityEngine;

public class Killfeed : MonoBehaviour
{
    [SerializeField] private GameObject killfeedItem;

	void Start ()
    {
        GameManager.instance.onPlayerKilledCallback += OnKill;
	}
	
	public void OnKill(string playerID, string sourceID)
    {
        Player victim = GameManager.GetPlayer(playerID);
        Player killer = GameManager.GetPlayer(sourceID);

        if (transform.childCount > 2)
            Destroy(transform.GetChild(0).gameObject);

        GameObject itemGO = Instantiate(killfeedItem, transform);
        KillfeedItem item = itemGO.GetComponent<KillfeedItem>();
        if (item) item.SetItem(victim, killer);

        Destroy(itemGO, 10f);
    }
}
