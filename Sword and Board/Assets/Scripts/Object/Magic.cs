using UnityEngine;

[CreateAssetMenu(fileName = "New Magic", menuName = "Equipment/Magic")]
public class Magic : Item
{
    [Space]
    public ScriptableObject mainAttack;
    public ScriptableObject specialAttack;
}