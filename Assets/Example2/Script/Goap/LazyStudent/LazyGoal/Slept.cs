using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Goal;
using Unity.GOAP.Agent;
public class Slept : CGoal
{
    public override void Initiate(CAgent a)
    {
        base.Initiate(a);
    }

    public override void OnStart()
    {
        base.OnStart();
    }
    public override void OnComplete()
    {
        base.OnComplete();
        IAgentExp2 student = (IAgentExp2)agent;
        student.ResetBoredom();
    }
}
