using UnityEngine;
using UnityEngine.UI;

public class SettingsChange : MonoBehaviour
{
    [SerializeField] private Slider mouseSensitivitySlider;
    [SerializeField] private Slider volumeSlider;

    [SerializeField] private Dropdown cameraModeDropdown;

    void Start ()
    {
        ReloadFormElements();
    }

    private void ReloadFormElements()
    {
        if (mouseSensitivitySlider)
            mouseSensitivitySlider.value = ClientSettings.mouseSensitivity;

        if (volumeSlider)
            volumeSlider.value = ClientSettings.volume;

        if (cameraModeDropdown)
            cameraModeDropdown.value = ClientSettings.defaultCamera;
    }

    public void SaveChanges()
    {
        ClientSettings.SaveChanges();
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

    public void SetVolume(float volume)
    {
        ClientSettings.volume = (byte)volume;
    }

    public void SetCameraMode(int cameraMode)
    {
        ClientSettings.defaultCamera = (byte)(cameraMode + 1);
    }
}
