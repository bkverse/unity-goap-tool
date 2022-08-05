using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Goal;
using Unity.GOAP.Agent;

public class WorkNightShift : CGoal
{

    public override void OnComplete()
    {
        base.OnComplete();
        IAgentExp2 worker = (IAgentExp2)agent;
        worker.EarnMoney(50);
    }
}

