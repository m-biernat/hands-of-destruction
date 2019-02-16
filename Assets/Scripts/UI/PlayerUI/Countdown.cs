using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Countdown : MonoBehaviour
{

    [SerializeField] private Text countdownText;
	
	void OnEnable()
    {
        StartCoroutine(BeginCountdown());
    }

    private IEnumerator BeginCountdown()
    {
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);

        gameObject.SetActive(false);
    }
}
