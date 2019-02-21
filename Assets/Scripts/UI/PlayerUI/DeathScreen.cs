using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private Text killerText;
    [SerializeField] private Text respawnTimeText;

    public void Setup(string killerName)
    {
        killerText.text = "YOU'VE BEEN KILLED BY" + "\n" 
            + "<size=34>" + killerName + "</size>";
        StartCoroutine(RespawnTime());
    }

    private IEnumerator RespawnTime()
    {
        float time = GameManager.instance.settings.respawnTime;

        while(time > 0f)
        {
            if (GameManager.instance.eventCode != 0) break;
            respawnTimeText.text = "RESPAWNING IN " + time;
            yield return new WaitForSeconds(1f);
            time--;
        }

        gameObject.SetActive(false);
    }
}
