using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Agent;
using Unity.GOAP.ActionBase;

public class PlayingAtPark : CActionBase
{
    float timer = 0;
    [SerializeField] float playTime = 5f;
    IAgentExp2 student;

    public override void Initiate(CAgent a)
    {
        base.Initiate(a);
        student = (IAgentExp2)agent;
    }
    public override bool Pre_Perform()
    {
        timer = 0;
        //if ((agent.agentFact.GetFact("MorningStudyTime").value == 1) ||
        //    (agent.agentFact.GetFact("AfternoonStudyTime").value == 1))
        //{
        //    student.IncSkipCounter();
        //}

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
        if ((agent.agentFact.GetFact("MorningStudyTime").value == 1) ||
            (agent.agentFact.GetFact("AfternoonStudyTime").value == 1))
        {
            student.IncSkipCounter();
        }

        timer += Time.deltaTime;
        if (timer >= playTime)
        {
            return true;
        }
        return false;
    }

    public override bool HasFailed()
    {
        return false;
    }

}
