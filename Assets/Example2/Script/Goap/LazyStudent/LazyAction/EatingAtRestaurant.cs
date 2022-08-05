using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Agent;
using Unity.GOAP.ActionBase;

public class EatingAtRestaurant : CActionBase
{
    IAgentExp2 student;
    float timer = 0f;
    [SerializeField] float eatTime = 2f;
    public override bool Pre_Perform()
    {
        student = (IAgentExp2)agent;
        timer = 0;
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
        timer += Time.deltaTime;
        if (agent.agentFact.GetFact("EatTime").value !=1 || timer >= eatTime)
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
