using System.Collections;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Football/" + (nameof(MaterialFactory)))]
public class MaterialFactory : ScriptableObject
{

    public MaterialBase[] materials;

    public MaterialBase GetMaterial(Enums.MaterialId material)
    {
        return materials.Where(x => x.materialId == material).FirstOrDefault();
    }
    
}

[System.Serializable]
public struct MaterialBase
{
    public string Name;
    public Enums.MaterialId materialId;
    public Material material;
}