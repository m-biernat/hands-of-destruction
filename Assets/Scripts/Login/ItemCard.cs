using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ItemCard : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private Image itemIconOutline;

    [SerializeField] private Text itemName;
    [SerializeField] private Text itemDescription;

    [SerializeField] private List<Text> itemAttributes;

    public void SetIcon(Sprite icon, Color color)
    {
        itemIcon.sprite = icon;
        itemIconOutline.color = color;
    }

    public void SetName(string name, Color color)
    {
        itemName.text = name;
        itemName.color = color;
    }

    public void SetDescription(string description)
    {
        itemDescription.text = description;
    }

    public void SetAttributes(List<string> attributes)
    {
        for(byte i = 0; i < itemAttributes.Count; i++)
        {
            if(i < attributes.Count)
            {
                itemAttributes[i].text = attributes[i];
            }
            else
            {
                itemAttributes[i].text = "";
            }
        }
    }
}
