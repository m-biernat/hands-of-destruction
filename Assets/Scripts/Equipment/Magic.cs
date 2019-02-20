using UnityEngine;

[CreateAssetMenu(fileName = "New Magic", menuName = "Equipment/Magic")]
public class Magic : Item
{
    [Space]
    public GameObject mainAttack;
    public GameObject specialAttack;

    [Space]
    public float mainAttackDamage = 20f;
    public float mainAttackSpeed = 18f;
    public float mainAttackDuration = 2f;

    [Space]
    public float specialAttackDamage = 40f;
    public float specialAttackMagickaCost = 30f;
    public float specialAttackSpeed = 12f;
    public float specialAttackDuration = 2f;
}