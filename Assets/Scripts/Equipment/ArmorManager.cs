using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Armor Manager", menuName = "Equipment/Manager/Armor Manager")]
public class ArmorManager : ScriptableObject
{
    public List<Armor> armorList;

    public Armor GetArmor(byte armorID)
    {
        if (armorList[armorID])
            return armorList[armorID];
        else
            return armorList[0];
    }

    public void AttachMesh(SkinnedMeshRenderer playerMesh, SkinnedMeshRenderer itemMesh)
    {
        if (itemMesh)
        {
            SkinnedMeshRenderer attachedMesh = Instantiate(itemMesh);
            attachedMesh.transform.parent = playerMesh.transform;

            attachedMesh.bones = playerMesh.bones;
            attachedMesh.rootBone = playerMesh.rootBone;
        }
    }
}
