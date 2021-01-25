using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

[ExecuteAlways]
public class FormationHandler : MonoBehaviour
{

    public static FormationHandler instance;

    public bool update;
    public Shooter[] shooters;
    public Shooter[] shootersOthers;

    public string currentFormationFormat;
    public Enums.FormationId FormationId;

    private string subPath = @"GameData\Formation";
    string FilePath { get { return Path.Combine(Application.dataPath , subPath); } }
    
    void Awake()
    {
        instance = this;
        ///print(FilePath);
    }

    public void UpdateShooterSnapPosition()
    {
        for (int i = 0; i < shooters.Length; i++)
        {
            Vector3 p = FindObjectOfType<PitchGridLayout>().GetSnapPosition(shooters[i].transform.position, new Vector2(1,0));
            p.y = shooters[i].transform.position.y;
            shooters[i].transform.position = p;
        }
    }

    public void SaveFormation()
    {

        List<Vector3> positions = new List<Vector3>();
        for (int i = 0; i < shooters.Length; i++)
        {
            positions.Add(shooters[i].transform.position);
        }
        Formation formation = new Formation() { format = currentFormationFormat,positions = positions, id = FormationId };

        string formationJson = JsonConvert.SerializeObject(formation);
        string pt = Path.Combine(FilePath, FormationId.ToString() + ".json");
        StreamWriter writer = new StreamWriter(pt);
        writer.Write(formationJson);
        writer.Close();
    }

    
    public void UpdateSquadFormation()
    {
        Formation formation = LoadFormation();

        for (int i = 0; i < shootersOthers.Length; i++)
        {
            Vector3 pos = formation.positions[i];
            pos.x = -pos.x;
            shootersOthers[i].transform.position = pos;
        }
        
    }

    public Formation LoadFormation()
    {
        return LoadFormation(FormationId);
        //string pt = Path.Combine(FilePath, FormationId.ToString() + ".json");
        //StreamReader reader = new StreamReader(pt);
        //string data = reader.ReadToEnd();
        //Formation formation = JsonConvert.DeserializeObject<Formation>(data);
        //return formation;
    }

    public Formation LoadFormation(Enums.FormationId formationId)
    {
        string pt = Path.Combine(FilePath, formationId.ToString() + ".json");
        StreamReader reader = new StreamReader(pt);
        string data = reader.ReadToEnd();
        Formation formation = JsonConvert.DeserializeObject<Formation>(data);
        return formation;
    }
}

[System.Serializable]
public class Formation
{
    public string format;
    public List<Vector3> positions;
    public Enums.FormationId id;

}
