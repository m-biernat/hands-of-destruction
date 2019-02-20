using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Magic Manager", menuName = "Equipment/Manager/Magic Manager")]
public class MagicManager : ScriptableObject
{
    public List<Magic> magicList;

    public Magic GetMagic(byte magicID)
    {
        if (magicList[magicID])
            return magicList[magicID];
        else
            return magicList[0];
    }
}
