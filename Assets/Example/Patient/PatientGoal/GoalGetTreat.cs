using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Goal;
using Unity.GOAP.Agent;

public class GoalGetTreat : CGoal
{
    // Right now, the goal state is edited in the Inspector, but can be edit in here
    public override void Initiate(CAgent a)
    {
        //this.goalName = "Get Treat";
        //this.important = 10;
        //this.deletable = true;

        //this.finished = false;
        //this.goals = new List<CFact>();
        //CFact g1 = new CFact("Arrived at the waiting area", 1);

        base.Initiate(a);
    }

}
