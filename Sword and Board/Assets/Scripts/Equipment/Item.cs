using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public List<SkinnedMeshRenderer> meshes = null;
}
