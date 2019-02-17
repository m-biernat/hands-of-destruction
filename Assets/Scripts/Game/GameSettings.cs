using UnityEngine;

[CreateAssetMenu(fileName = "New Game Setting", menuName = "Game/Setting")]
public class GameSettings : ScriptableObject
{
    [Header("Game objective settings")]
    public short pointsToComplete = 50;
    public short pointsMultiplier = 1;

    [Space(10)]
    public int scoreMultiplier = 5;

    [Space(10)]
    public float respawnTime = 3f;

    [Space(10)]
    public string objectiveText = "";

    [Header("Team manager settings")]
    public bool teamAssignEnabled = true;

    [Header("Duration of game stages (in seconds)")]
    public short prepareTime = 60;
    public short gameplayTime = 600;
    public short completeTime = 15;
}
