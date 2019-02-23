using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EffectsUI : MonoBehaviour
{
    [SerializeField] private Image effectsImage;

    [SerializeField] private GameObject deathScreen;

    [SerializeField] private Color defaultColor;
    [SerializeField] private Color damageColor;
    [SerializeField] private Color deathScreenColor;

    private bool deathScreenActive = false;

    public void ShowDeathScreen(string killerName)
    {
        deathScreenActive = true;
        effectsImage.color = deathScreenColor;
        deathScreen.SetActive(true);
        deathScreen.GetComponent<DeathScreen>().Setup(killerName);
    }

    public void ClearEffects()
    {
        deathScreenActive = false;
        effectsImage.color = defaultColor;
    }

    public void DamageEffect()
    {
        StartCoroutine(Fade(0f, damageColor.a, .25f));
        StartCoroutine(Fade(damageColor.a, 0f, .25f));
    }

    private IEnumerator Fade(float start, float end, float lerpTime)
    {
        float timeStartedLerp = Time.time;
        float timeSinceStarted = Time.time - timeStartedLerp;
        float percentageComplete = timeSinceStarted / lerpTime;

        Color alphaColor = damageColor;

        while (true)
        {
            if (deathScreenActive) break;

            timeSinceStarted = Time.time - timeStartedLerp;
            percentageComplete = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentageComplete);

            alphaColor.a = currentValue;
            effectsImage.color = alphaColor;

            if (percentageComplete >= 1) break;

            yield return new WaitForEndOfFrame();
        }
    }
}
