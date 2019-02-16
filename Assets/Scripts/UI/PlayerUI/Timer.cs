using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text timerText;

    [SerializeField] private Color defaultColor;
    [SerializeField] private Color warningColor;

    void LateUpdate()
    {
        SetTime();
    }

    private void SetTime()
    {
        int time = GameManager.instance.timeCounter;
        int minutes = 0, seconds = 0;

        seconds = time % 60;
        minutes = (time - seconds) / 60;

        timerText.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);

        if (time <= 5)
            timerText.color = warningColor;
        else
            timerText.color = defaultColor;
    }
}
