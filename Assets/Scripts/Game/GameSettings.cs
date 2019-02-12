using UnityEngine;

[CreateAssetMenu(fileName = "New Game Setting", menuName = "Game/Setting")]
public class GameSettings : ScriptableObject
{
    public int pointsAtStart;
    public int pointsToComplete;
    public int pointsMultiplier;

    public bool teamAssignEnabled;
}
