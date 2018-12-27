using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(ItemCard))]
public class MagicCardData : MonoBehaviour
{
    private ItemCard itemCard;

    [SerializeField] private Magic item;

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
        List<string> attributes = new List<string>();

        return attributes;
    }
}
