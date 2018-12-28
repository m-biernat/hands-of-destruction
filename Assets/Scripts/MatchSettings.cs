using UnityEngine;

[CreateAssetMenu(fileName = "New Match Setting", menuName = "Match/Setting")]
public class MatchSettings : ScriptableObject
{
    public int pointsAtStart;
    public int pointsToComplete;
    public int pointsMultiplier;

    public bool teamAssignEnabled;
}
