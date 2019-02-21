using UnityEngine;
using UnityEngine.UI;

public class EffectsUI : MonoBehaviour
{
    [SerializeField] private Image effectsImage;

    [SerializeField] private GameObject deathScreen;

    [SerializeField] private Color defaultColor;
    [SerializeField] private Color deathScreenColor;

    public void ShowDeathScreen(string killerName)
    {
        effectsImage.color = deathScreenColor;
        deathScreen.SetActive(true);
        deathScreen.GetComponent<DeathScreen>().Setup(killerName);
    }

    public void ClearEffects()
    {
        effectsImage.color = defaultColor;
    }
}
