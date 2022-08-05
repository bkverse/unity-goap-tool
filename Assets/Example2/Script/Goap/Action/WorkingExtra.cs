using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.GOAP.ActionBase;
using Unity.GOAP.Agent;
using Unity.GOAP.World;
public class WorkingExtra : CActionBase
{
    float timer = 0f;
    [SerializeField] float workTime = 10f;
    public WorkingExtra() : base()
    {
        this.actionName = "WorkingExtra";
    }
    public override bool Pre_Perform()
    {
        return base.Pre_Perform();
    }

    public override bool PerformAction()
    {
        return base.PerformAction();
    }

    public override bool Pos_Perform()
    {
        agent.agentFact.ChangeFact("needMoney", 0);
        return base.Pos_Perform();
    }
    public override bool HasCompleted()
    {
        timer += Time.deltaTime;
        if (timer >= workTime)
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