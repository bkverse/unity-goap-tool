using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Goal;
using Unity.GOAP.Agent;
public class WorkExtraHours : CGoal
{
    public override void Initiate(CAgent a)
    {
        base.Initiate(a);
    }

    public override void OnComplete()
    {
        base.OnComplete();
        IAgentExp2 worker = (IAgentExp2)agent;
        worker.EarnMoney(0);
    }
}

