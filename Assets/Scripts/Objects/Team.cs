using System.Collections;
using UnityEngine;


public class TeamInfo
{
    public string PlayerName;
    public Material teamMat;
    public Enums.FormationId formation;
    public int shootersCount;
}

public class Team : MonoBehaviour
{
    public string PlayerName;
    public Enums.Team team;
    public Enums.FormationId formation;
    public Material teamMat;
    public Shooter[] shooters;
    public AnimationCurve turnAnimation;

    [HideInInspector] public Enums.GoalId goalId;
    [HideInInspector]public float goalCount;
    [HideInInspector]public Vector3[] formationPositions;
    [HideInInspector]public int shootersCount;
    [HideInInspector]public bool isTurn;
    float animTime;
    
    


    public void Update()
    {
        if (isTurn)
        {
            animTime = (animTime + Time.deltaTime) % 1;
            float percent = MatchHandler.instance.turnAnimation.Evaluate(animTime);
            for (int i = 0; i < shooters.Length; i++)
            {
                shooters[i].UpdateIndicator(percent);
            }
        }
    }


    public void TeamFixedUpdate()
    {
        for (int i = 0; i < shooters.Length; i++)
        {
            shooters[i].ShooterUpdate();
        }
    }

    public void Setup(TeamInfo info)
    {

        formation = info.formation;
        PlayerName = info.PlayerName;
        shootersCount = info.shootersCount;
        teamMat = info.teamMat;

        Formation formationA = MainSceneManager.instance.formationFactory.GetFormation(info.formation);
        formationPositions = formationA.positions.ToArray();

        shooters = new Shooter[shootersCount];
        for (int i = 0; i < shootersCount; i++)
        {
            Shooter shooter = MainSceneManager.instance.objectFactory.GetShooter();
            shooters[i] = shooter;
            shooter.transform.parent = transform;
            Vector3 pos = formationPositions[i];
            pos.x = (team == Enums.Team.Left) ? pos.x : -pos.x;
            shooter.transform.position = pos;
        }

        for (int i = 0; i < shooters.Length; i++)
        {
            shooters[i].SetMaterial(teamMat);
        }
        for (int i = 0; i < shooters.Length; i++)
        {
            shooters[i].SetMaterial(teamMat);
        }

        
        
        //ResetSquadPositions();
    }

    public void SetTurn(bool value)
    {
        isTurn = value;
        for (int i = 0; i < shooters.Length; i++)
        {
            shooters[i].SetShooterTurn(value);
            shooters[i].ActivateTurnUndicator(value);
        }
        animTime = 0;
    }

    

    public void ResetSquadPositions()
    {
        StartCoroutine(ResetPositions());
    }

    IEnumerator ResetPositions()
    {

        float t = 0;
        while (t <= 1)
        {
            t += Time.deltaTime * .5f;

            for (int i = 0; i < shooters.Length; i++)
            {
                Vector3 pos = formationPositions[i];
                pos.x = (team == Enums.Team.Left) ? pos.x : -pos.x;
                pos.y = shooters[i].transform.position.y;
                shooters[i].transform.position = Vector3.Lerp(shooters[i].transform.position, pos, t);
            }
            yield return null;
        }

        for (int i = 0; i < shooters.Length; i++)
        {
            Vector3 pos = formationPositions[i];
            pos.x = (team == Enums.Team.Left) ? pos.x : -pos.x;
            pos.y = shooters[i].transform.position.y;
            shooters[i].transform.position = pos;
        }

    }
}