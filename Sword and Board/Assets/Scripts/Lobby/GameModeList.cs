using UnityEngine;
using System.Collections.Generic;

public class GameModeList : MonoBehaviour
{
    [Header("List of available game modes")]
    [SerializeField] private List<string> sceneNames = new List<string>();

    public List<string> GetGameModeList()
    {
        return sceneNames;
    }
}
