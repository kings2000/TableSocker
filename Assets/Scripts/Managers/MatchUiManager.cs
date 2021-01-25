using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MatchUiManager : MonoBehaviour
{

    public static MatchUiManager instance;
    public Text timeDisplay;
    public TeamUi[] teams;

    private Enums.Team currentTeam;
    Coroutine coolDownRoutine;

    void Start()
    {
        instance = this;
    }

    public void UpdateTime(string time)
    {
        timeDisplay.text = time;
    }

    public void SetCurrentTurn(Enums.Team team, float coolDown)
    {
        currentTeam = team;
        coolDownRoutine = StartCoroutine(StartCoolDown(teams.Where(x => x.team == team).FirstOrDefault(),coolDown));
    }

    public void StopCooldown()
    {
        if(coolDownRoutine != null)
        {
            StopCoroutine(coolDownRoutine);
        }
        coolDownRoutine = null;
    }

    public void UpdateTeamGoalCount(Team t)
    {
        print(t.team);
        Text ui = teams.Where(x => x.team == t.team).FirstOrDefault().goalScored;
        ui.text = t.goalCount.ToString();
    }

    IEnumerator StartCoolDown(TeamUi teamUi ,float coolDownTime)
    {
        float totalTime = coolDownTime;
        totalTime -= Time.deltaTime;
        while(totalTime > 0)
        {
            float percent = Helper.GetRatePercent01(totalTime, 0, coolDownTime);
            teamUi.filler.fillAmount = percent;
            yield return null;
        }
        
        //Stop the turn
        if(totalTime <= 0)
        {
            MatchHandler.instance.OnCooldownDone(currentTeam);
            currentTeam = Enums.Team.None;
        }
    }

}

[System.Serializable]
public struct TeamUi
{
    public Enums.Team team;
    public Image filler;
    public Text goalScored;

}
