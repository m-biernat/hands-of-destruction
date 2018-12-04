using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Equipment/Weapon")]
public class Weapon : Item
{
    public float damageModifier = 0f;
    public float rangeModifier = 0f;

    public AnimatorOverrideController animationsOverride = null;
}
