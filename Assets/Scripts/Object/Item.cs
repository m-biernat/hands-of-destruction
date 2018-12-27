using UnityEngine;

public abstract class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public Color color = Color.white;
    public string description = "";
}
