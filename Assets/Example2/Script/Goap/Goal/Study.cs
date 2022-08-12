using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Goal;
using Unity.GOAP.Agent;
public class Study: CGoal
{

    public override void OnComplete()
    {
        if (agent.goalList.FindIndex(f=>f.goalName == "SkipSchoolAndPlay") >= 0)
        {
            agent.UpdateGoalImportant("SkipSchoolAndPlay", 76);
            agent.UpdateGoalImportant(this.goalName, 50);
        }
        base.OnComplete();
    }
}

