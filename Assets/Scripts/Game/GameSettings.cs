using UnityEngine;

[CreateAssetMenu(fileName = "New Game Setting", menuName = "Game/Setting")]
public class GameSettings : ScriptableObject
{
    public short pointsAtStart = 0;
    public short pointsToComplete = 50;
    public int pointsMultiplier = 1;

    public bool teamAssignEnabled = true;


}
