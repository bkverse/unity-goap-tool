using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.GOAP.ActionBase;
using Unity.GOAP.Agent;
using Unity.GOAP.World;
public class WorkingInAfternoon : CActionBase
{
    public WorkingInAfternoon() : base()
    {
        this.actionName = "WorkingInAfternoon";
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
        return base.Pos_Perform();
    }
    public override bool HasCompleted()
    {
        if (CWorld.Instance.GetFacts().GetFact("afternoonShift").value == 0)
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