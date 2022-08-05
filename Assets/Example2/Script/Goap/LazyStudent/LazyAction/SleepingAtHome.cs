using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Agent;
using Unity.GOAP.ActionBase;

public class SleepingAtHome : CActionBase
{
    IAgentExp2 student;
    public override bool Pre_Perform()
    {
        student = (IAgentExp2)agent;
        return true;
    }

    public override bool PerformAction()
    {
        isActive = true;
        return true;
    }

    public override bool Pos_Perform()
    {
        return base.Pos_Perform();
    }
    public override bool HasCompleted()
    {
        if (agent.agentFact.GetFact("SleepTime").value != 1)
        {
            return true;
        }

        if ((agent.agentFact.GetFact("MorningStudyTime").value == 1) ||
            (agent.agentFact.GetFact("AfternoonStudyTime").value == 1))
        {
            student.IncSkipCounter();
        }

        return false;
    }

    public override bool HasFailed()
    {
        return false;
    }

}
