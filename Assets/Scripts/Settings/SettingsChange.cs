using UnityEngine;
using UnityEngine.UI;

public class SettingsChange : MonoBehaviour
{
    [SerializeField] private Slider mouseSensitivitySlider;
    [SerializeField] private Slider brightnessSlider;

    [SerializeField] private Dropdown cameraModeDropdown;

    void Start ()
    {
        ReloadFormElements();
    }

    private void ReloadFormElements()
    {
        if (mouseSensitivitySlider)
            mouseSensitivitySlider.value = ClientSettings.mouseSensitivity;

        if (brightnessSlider)
            brightnessSlider.value = ClientSettings.brightness;

        if (cameraModeDropdown)
            cameraModeDropdown.value = ClientSettings.defaultCamera;
    }

    public void SaveChanges()
    {
        ClientSettings.SaveChanges();
        ReloadFormElements();
    }

    public void ResetChanges()
    {
        ClientSettings.Load();
        ReloadFormElements();
    }

    public void LoadDefaults()
    {
        ClientSettings.LoadDefaults();
        ReloadFormElements();
    }

    public void SetMouseSensitivity(float mouseSensitivity)
    {
        ClientSettings.mouseSensitivity = Mathf.Floor(mouseSensitivity * 10f) / 10f;
    }

    public void SetBrightness(float brightness)
    {
        ClientSettings.brightness = (sbyte)brightness;
    }

    public void SetCameraMode(int cameraMode)
    {
        ClientSettings.defaultCamera = (byte)(cameraMode + 1);
    }
}
