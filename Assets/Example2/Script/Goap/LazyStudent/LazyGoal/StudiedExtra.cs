using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Goal;
using Unity.GOAP.Agent;

public class StudiedExtra : CGoal
{
    public override void OnStart()
    {

        base.OnStart();
    }
    public override void OnComplete()
    {
        base.OnComplete();
        IAgentExp2 student = (IAgentExp2)agent;
        student.ResetSkipCounter();
    }
}
