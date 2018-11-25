using UnityEngine;
using UnityEngine.UI;

public class GameModeSelect : MonoBehaviour
{
    private Dropdown dropdown;

    private string selectedGameMode;

    public string Label { get; private set; }
    public string Func { get; private set; }

    void Start()
    {
        dropdown = GetComponent<Dropdown>();

        Label = dropdown.options[0].text;
        Func = dropdown.options[1].text;

        selectedGameMode = Label;
    }

    public void SetValue(Dropdown option)
    {
        selectedGameMode = option.itemText.text;
    }

    public string GetValue()
    {
        return selectedGameMode;
    }
}
