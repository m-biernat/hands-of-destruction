using UnityEngine;
using System.Collections.Generic;

public class GameModeList : MonoBehaviour
{
    [System.Serializable]
    public struct GameModes
    {
        public string modeName;
        public string sceneName;
        public bool inDevelopment;
    }

    [SerializeField] private GameModes[] gameModes;

    public List<string> GetGameModeList()
    {
        List<string> gameModeList = new List<string>();

        foreach(var elem in gameModes)
        {
            if (!Debug.isDebugBuild && elem.inDevelopment) continue;
            gameModeList.Add(elem.modeName);
        }

        return gameModeList;
    }

    public Dictionary<string, string> GetGameModes()
    {
        Dictionary<string, string> gameModeDict = new Dictionary<string, string>();

        foreach(var elem in gameModes)
        {
            gameModeDict.Add(elem.modeName, elem.sceneName);
        }

        return gameModeDict;
    }
}
