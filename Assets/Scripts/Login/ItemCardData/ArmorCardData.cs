using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(ItemCard))]
public class ArmorCardData : MonoBehaviour
{
    private ItemCard itemCard;

    [SerializeField] private Armor item;

    void Start()
    {
        itemCard = GetComponent<ItemCard>();

        itemCard.SetIcon(item.icon, item.color);
        itemCard.SetName(item.name, item.color);
        itemCard.SetDescription(item.description);

        itemCard.SetAttributes(GetAttributes());
    }

    private List<string> GetAttributes()
    {
        Dictionary<string, float> armorAttributes = item.GetModifiers();
        List<string> attributes =  new List<string>();

        foreach(var attrib in armorAttributes)
        {
            if (attrib.Value != 0f)
            {
                string sign = "";

                if (attrib.Value > 0f)
                    sign = "+";

                string val = (Mathf.Floor(attrib.Value * 100)).ToString() + '%';
                attributes.Add(attrib.Key + " " + sign + val);
            }
        }

        return attributes;
    }
}
