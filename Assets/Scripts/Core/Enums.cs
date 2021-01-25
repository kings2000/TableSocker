using System.Collections;
using UnityEngine;

namespace Enums
{
    public enum Team
    {
        None,
        Left,
        Right
    }

    public enum GoalId
    {
        Right,
        Left
    }

    public enum FormationId
    {
        f_1,
        f_2,
        f_3,
    }

    public enum SceneId
    {
        Main = 1,
        MatchUi = 2,
        Map1 = 3,
    }

    public enum MaterialId
    {
        Chelsae,
        Manu
    }

    public enum MatchType
    {
        TimeedMatch,
        GoalMatch
    }
}