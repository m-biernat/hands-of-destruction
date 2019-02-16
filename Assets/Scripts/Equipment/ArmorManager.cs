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

    [SerializeField] private Material teamColorMaterial = null;

    [SerializeField] private Color teamRedColor = Color.red;
    [SerializeField] private Color teamBlueColor = Color.blue;

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

    public void SetTeamColor(SkinnedMeshRenderer playerMesh, byte teamID)
    {
        if (GameManager.instance.settings.teamAssignEnabled && teamID != 0)
        {
            SkinnedMeshRenderer armorInstance = 
                playerMesh.gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();

            int matIndex = GetTeamColorMaterialIndex(armorInstance);

            if (matIndex != -1)
            {
                if (teamID == 1)
                    armorInstance.materials[matIndex].color = teamRedColor;
                if (teamID == 2)
                    armorInstance.materials[matIndex].color = teamBlueColor;
            }
        }
    }

    private int GetTeamColorMaterialIndex(SkinnedMeshRenderer armorInstance)
    {
        int ind = 0;

        foreach (var mat in armorInstance.materials)
        {
            if (mat.name == (teamColorMaterial.name + " (Instance)"))
                return ind;
            ind++;
        }

        Debug.LogError("Team Color material is missing!");
        return -1;
    }
}
