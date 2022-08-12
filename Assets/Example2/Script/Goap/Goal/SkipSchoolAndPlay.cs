using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Goal;
using Unity.GOAP.Agent;

public class SkipSchoolAndPlay : CGoal
{
    int count = 0;
    public SkipSchoolAndPlay() : base()
    {
        this.goalName = "SkipSchoolAndPlay";
    }

    public override void OnComplete()
    {
        agent.UpdateGoalImportant("StudyingAfternoon", 76);
        agent.UpdateGoalImportant(this.goalName, 50);
        count++;
        if (count >= 3)
        {
            agent.UpdateGoalImportant("StudyingExtraClass", 77);
            count = 0;
        }
        base.OnComplete();
    }
}

