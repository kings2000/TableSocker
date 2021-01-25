using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MatchHandler : MonoBehaviour
{

    public static MatchHandler instance;
    public const string GoalRightTag = "GoalRight";
    public const string GoalLeftTag = "GoalLeft";

    public int matchTimeMin;
    public AnimationCurve turnAnimation;

    [HideInInspector]public Team teamLeft;
    [HideInInspector]public Team teamRight;
    [HideInInspector]public Ball ball;

    //Timed Match
    private const int totalTimeMin = 90;
    private const int totalTimeSec = 5400;
    private int currentMatchTimeSec;
    private int MatchTimeSec;
    private float timeCountDownSpeed;
    private float timeTracker;
    private bool timePaused = true;
    private Enums.Team currentTeamTurn;
    private float objectMotionStateUpdateTime;
    private bool updateObjectMotion;

    private Dictionary<Enums.GoalId, Team> goals;

    private bool updateGaols;
    private bool kickOff;
    private bool canChangeTurn;
    private bool initialised = false;

    DateTime timeTotal;
    DateTime startTime;

    public void PauseTime(bool value) => timePaused = value;

    private void Awake()
    {
        instance = this;
    }


    public void Init()
    {
        return;
        updateGaols = true;
        timeCountDownSpeed = ((float)matchTimeMin / (float)totalTimeMin);
        timeTotal = DateTime.UtcNow.AddMinutes(matchTimeMin);
        startTime = DateTime.UtcNow;
        ball = FindObjectOfType<Ball>();
        kickOff = true;
        canChangeTurn = true;

        TeamInfo info1 = new TeamInfo();
        info1.formation = Enums.FormationId.f_2;
        info1.PlayerName = "Playes";
        info1.shootersCount = 5;
        info1.teamMat = MainSceneManager.instance.materialFactory.GetMaterial(Enums.MaterialId.Chelsae).material;

        TeamInfo info2 = new TeamInfo();
        info2.formation = Enums.FormationId.f_2;
        info2.PlayerName = "Playes";
        info2.shootersCount = 5;
        info2.teamMat = MainSceneManager.instance.materialFactory.GetMaterial(Enums.MaterialId.Manu).material;

        SetUpTeam(info1, info2);
        initialised = true;
    }


    public void SetUpTeam(TeamInfo _leftTeam, TeamInfo _rightTeam)
    {

        Team rightTeam = new GameObject("RightTeam").AddComponent<Team>();
        Team leftTeam = new GameObject("leftTeam").AddComponent<Team>();

        teamRight = rightTeam;
        teamLeft = leftTeam;
        teamRight.team = Enums.Team.Right;
        teamLeft.team = Enums.Team.Left;

        goals = new Dictionary<Enums.GoalId, Team>();

        //goals to score fot points adding
        goals.Add(Enums.GoalId.Left, teamRight); 
        goals.Add(Enums.GoalId.Right, teamLeft);

        teamRight.goalId = Enums.GoalId.Right;
        teamLeft.goalId = Enums.GoalId.Left;

        teamRight.Setup(_rightTeam);
        teamLeft.Setup(_leftTeam);
    }

    public void SwitchTeamSides()
    {
        Team A = teamRight;
        teamRight = teamLeft;
        teamLeft = A;
        goals = new Dictionary<Enums.GoalId, Team>();
        goals.Add(Enums.GoalId.Right, teamLeft);
        goals.Add(Enums.GoalId.Left, teamRight);
    }

    public void StartGame()
    {
        timePaused = false;
        ChoosheRamdonTurn();
    }

    void ChoosheRamdonTurn()
    {
        SetCurrentTurn((Enums.Team)UnityEngine.Random.Range(1,3));
    }

    public void InitiatChangeTurn()
    {
        //do some valid position check
        ChangeTurn();
    }

    public void ChangeTurn(bool afterGaol = false)
    {
        if (!canChangeTurn && !afterGaol)
        {
            return;
        }

        if (!afterGaol)
        {
            kickOff = false;
        }

        if (currentTeamTurn == Enums.Team.Left)
        {
            SetCurrentTurn(Enums.Team.Right);
        }
        else if(currentTeamTurn == Enums.Team.Right)
        {
            SetCurrentTurn(Enums.Team.Left);
        }
        canChangeTurn = true;
    }

    public void SetCurrentTurn(Enums.Team team)
    {
        currentTeamTurn = team;
        if(currentTeamTurn == Enums.Team.Left)
        {
            
            teamRight.SetTurn(true);
        }
        else
        {
            
            teamLeft.SetTurn(true);
        }
    }

    public void DisableShooterTurn()
    {
        
        if (currentTeamTurn == Enums.Team.Left)
        {
            teamRight.SetTurn(false);
        }
        else
        {
            teamLeft.SetTurn(false);
        }
        objectMotionStateUpdateTime = 0.5f;
        updateObjectMotion = true;
    }

    public void MatchEnd()
    {

    }

    public void UpdateMatchTime()
    {
        
        TimeSpan span = Helper.GetLaspTime(timeTotal);
        MatchUiManager.instance.UpdateTime(Helper.TimeFormat(span));
        if(span.TotalSeconds <= 0)
        {
            //MatchEnd
            MatchEnd();
        }
    }

    public void ObjectInMotion()
    {
        print("Up");
        objectMotionStateUpdateTime = 0.5f;
    }

    private void Update()
    {
        if(!timePaused)
            UpdateMatchTime();

        if (updateObjectMotion)
        {
            if (objectMotionStateUpdateTime <= 0)
            {
                updateObjectMotion = false;
                InitiatChangeTurn();
            }
            objectMotionStateUpdateTime -= Time.deltaTime;
        }
        
    }

    private void FixedUpdate()
    {
        if (initialised)
        {
            ball.BallUpdate();
            teamLeft.TeamFixedUpdate();
            teamRight.TeamFixedUpdate();
        }
        

    }

    public void OnCooldownDone(Enums.Team team)
    {
        if(team == currentTeamTurn)
        {

        }
    }

    public void OnGoal(string goalTag)
    {
        bool foul = kickOff;
        if (kickOff)
        {
            print("Foul");
        }

        if (!updateGaols) return;
        timePaused = true;
        updateGaols = false;
        Team team = null;
        kickOff = true;
        canChangeTurn = false;

        if (!foul)
        {
            
            if (goalTag == GoalLeftTag)
            {
                
                team = goals[Enums.GoalId.Left];
                team.goalCount++;
                currentTeamTurn = team.team;
            }
            else if (goalTag == GoalRightTag)
            {
                team = goals[Enums.GoalId.Right];
                team.goalCount++;
                currentTeamTurn = team.team;
            }
            MatchUiManager.instance.UpdateTeamGoalCount(team);
        }
        
        
        Invoke(nameof(ResetSquadPositions),2);
    }

    public void ResetSquadPositions()
    {
        StartCoroutine(ResetPositions());
        teamLeft.ResetSquadPositions();
        teamRight.ResetSquadPositions();
    }

    IEnumerator ResetPositions()
    {
        
        float t = 0;
        while (t <= 1)
        {
            t += Time.deltaTime * .3f;
            ball.transform.position = Vector3.Lerp(ball.transform.position, new Vector3(0, ball.transform.position.y, 0), t);
            yield return null;
        }
        timePaused = false;
        updateGaols = true;
        ChangeTurn(true);
    }
}


