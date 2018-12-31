using UnityEngine;
using UnityEngine.UI;

public class Resources : MonoBehaviour
{
    private Player player;

    [SerializeField] private Text healthValue;
    [SerializeField] private Text staminaValue;
    [SerializeField] private Text magickaValue;

    [SerializeField] private RectTransform healthFill;
    [SerializeField] private RectTransform staminaFill;
    [SerializeField] private RectTransform magickaFill;

    private float maxHealth, maxStamina, maxMagicka;

    void Start()
    {
        player = PlayerUI.playerComponent;

        maxHealth = player.GetMaxHealth();
        maxStamina = player.GetMaxStamina();
        maxMagicka = player.GetMaxMagicka();
    }

    void Update()
    {
        SetResourceBar(player.Health, maxHealth, healthFill, healthValue);
        SetResourceBar(player.Stamina, maxStamina, staminaFill, staminaValue);
        SetResourceBar(player.Magicka, maxMagicka, magickaFill, magickaValue);
    }

    private void SetResourceBar(float value, float limit, RectTransform resourceBar, Text resourceText)
    {
        resourceText.text = value.ToString();
        resourceBar.localScale = new Vector3(value / limit, 1f, 1f);
    }
}
