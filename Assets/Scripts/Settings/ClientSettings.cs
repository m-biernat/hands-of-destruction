using UnityEngine;

public static class ClientSettings
{
    private const string DEFAULT_PLAYER_NAME = "";

    private const float DEFAULT_M_SENSITIVITY = 6f;
    private const byte DEFAULT_CAMERA_MODE = 2;

    private const byte DEFAULT_VOLUME = 0;


    public static string playerName = DEFAULT_PLAYER_NAME;

    public static byte selectedMagicID = 0, selectedArmorID = 0;

    public static float mouseSensitivity = DEFAULT_M_SENSITIVITY;
    public static byte defaultCamera = DEFAULT_CAMERA_MODE;

    public static byte volume = DEFAULT_VOLUME;


    public static void Load()
    {
        if (PlayerPrefs.HasKey("playerName"))
        {
            playerName = PlayerPrefs.GetString("playerName");
            mouseSensitivity = PlayerPrefs.GetFloat("mouseSensitivity");
            defaultCamera = (byte)PlayerPrefs.GetInt("defaultCamera");
            volume = (byte)PlayerPrefs.GetInt("volume");
        }
    }

    public static void SaveChanges()
    {
        PlayerPrefs.SetString("playerName", playerName);
        PlayerPrefs.SetFloat("mouseSensitivity", mouseSensitivity);
        PlayerPrefs.SetInt("defaultCamera", defaultCamera);
        PlayerPrefs.SetInt("volume", volume);
    }

    public static void LoadDefaults()
    {
        mouseSensitivity = DEFAULT_M_SENSITIVITY;
        defaultCamera = DEFAULT_CAMERA_MODE;
        volume = DEFAULT_VOLUME;
    }
}
