using UnityEngine;

[CreateAssetMenu(fileName = "New Game Setting", menuName = "Game/Setting")]
public class GameSettings : ScriptableObject
{
    public int pointsAtStart = 0;
    public int pointsToComplete = 50;
    public int pointsMultiplier = 1;

    public bool teamAssignEnabled = true;
}
