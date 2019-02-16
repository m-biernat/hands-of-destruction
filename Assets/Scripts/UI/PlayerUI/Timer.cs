using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text timerText;

    void LateUpdate()
    {
        timerText.text = SetTime();
    }

    private string SetTime()
    {
        int time = GameManager.instance.timeCounter;
        int minutes = 0, seconds = 0;

        seconds = time % 60;
        minutes = (time - seconds) / 60;

        return string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }
}
