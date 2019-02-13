using UnityEngine;
using UnityEngine.UI;

public class PlayerNameplate : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private Text nameplateText;
    [SerializeField] private Image nameplateImage;

    [SerializeField] private RectTransform healthBarFill;

    [SerializeField] private Color teamRedColor;
    [SerializeField] private Color teamBlueColor;

    private float maxHealth;

    private GameObject cameras;

    void Start ()
    {
        nameplateText.text = player.playerName;
        maxHealth = player.GetMaxHealth();
        SetNameplateColor();

        cameras = GameObject.FindGameObjectWithTag("Cameras");
    }

	void Update ()
    {
        healthBarFill.localScale = new Vector3(player.Health / maxHealth, 1f, 1f);
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + cameras.transform.rotation * Vector3.forward,
            cameras.transform.rotation * Vector3.up);
    }

    private void SetNameplateColor()
    {
        if (player.teamID == 1)
            nameplateImage.color = teamRedColor;
        if (player.teamID == 2)
            nameplateImage.color = teamBlueColor;
    }
}
