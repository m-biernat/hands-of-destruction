using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Armor", menuName = "Equipment/Armor")]
public class Armor : Item
{  
    public List<SkinnedMeshRenderer> meshes = null;

    [Header("Modifies attribute by percent")]
    public float healthModifier = 0f;
    public float staminaModifier = 0f;
    public float magickaModifier = 0f;
    [Space]
    public float staminaRegenModifier = 0f;
    public float magickaRegenModifier = 0f;
    [Space]
    public float speedModifier = 0f;

    public Dictionary<string,float> GetModifiers()
    {
        Dictionary<string, float> modifiers = new Dictionary<string, float>();

        modifiers.Add("Health", healthModifier);
        modifiers.Add("Stamina", staminaModifier);
        modifiers.Add("Magicka", magickaModifier);

        modifiers.Add("Stamina Regen", staminaRegenModifier);
        modifiers.Add("Magicka Regen", magickaRegenModifier);

        modifiers.Add("Speed", speedModifier);

        return modifiers;
    }
}