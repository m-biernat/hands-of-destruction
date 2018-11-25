using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections.Generic;


public class GameModes : MonoBehaviour
{
    [SerializeField] private Dropdown quickJoinDropdown;
    [SerializeField] private Dropdown createGameDropdown;

    [SerializeField] private List<SceneAsset> scenes = new List<SceneAsset>();
    private List<string> sceneNames = new List<string>();

    void Start ()
    {
        ParseScenes();
        FillDropdown(quickJoinDropdown);
        FillDropdown(createGameDropdown);
	}
	
    private void ParseScenes()
    {
        foreach (var scene in scenes) sceneNames.Add(scene.name);
    }

    private void FillDropdown(Dropdown dropdown)
    {
        dropdown.AddOptions(sceneNames);
    }

    public string GetRandom()
    {
        if (sceneNames.Count > 0)
            return sceneNames[Random.Range(0, sceneNames.Count)];
        else
            return null;
    }
}
