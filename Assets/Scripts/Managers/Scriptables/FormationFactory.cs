using System.Collections;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;

[CreateAssetMenu(menuName = "Football/" + (nameof(FormationFactory)))]
public class FormationFactory : ScriptableObject
{

    public FormationData[] formations;

    public Formation GetFormation(Enums.FormationId formationId)
    {
        FormationData f = formations.Where(x => x.formationId == formationId).FirstOrDefault();
        Formation formation = JsonConvert.DeserializeObject<Formation>(f.data.text);
        return formation;
    }
}

[System.Serializable]
public struct FormationData
{
    public TextAsset data;
    public Enums.FormationId formationId;
}