using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Goal;
using Unity.GOAP.Agent;

public class Ate : CGoal
{
    IAgentExp2 student;
    public override void OnComplete()
    {
        base.OnComplete();

        student = (IAgentExp2)agent;
        student.ResetHunger();
    }
}
