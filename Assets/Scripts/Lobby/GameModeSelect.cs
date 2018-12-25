using UnityEngine;
using UnityEngine.UI;

public class GameModeSelect : MonoBehaviour
{
    private Dropdown dropdown;

    [SerializeField] private GameModeList gameModes;

    private string selectedGameMode;

    public string Label { get; private set; }
    public string Func { get; private set; }

    void Start()
    {
        dropdown = GetComponent<Dropdown>();

        dropdown.AddOptions(gameModes.GetGameModeList());

        Label = dropdown.options[0].text;
        Func = dropdown.options[1].text;

        selectedGameMode = Label;
    }

    public void SetValue(int option)
    {
        selectedGameMode = dropdown.options[option].text;
    }

    public string GetValue()
    {
        return selectedGameMode;
    }

    public void SetRandom()
    {
        int rand = Random.Range(2, dropdown.options.Count - 1);
        selectedGameMode = dropdown.options[rand].text;
    }
}
