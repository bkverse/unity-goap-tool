using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Goal;
using Unity.GOAP.Agent;

public class Eat : CGoal
{
    IAgentExp2 worker;

    public override void OnComplete()
    {
        base.OnComplete();
        worker = (IAgentExp2)agent;
        worker.ResetHunger();
    }
}

