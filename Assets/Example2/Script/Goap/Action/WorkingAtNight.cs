using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.GOAP.ActionBase;
using Unity.GOAP.Agent;
using Unity.GOAP.World;
public class WorkingAtNight : CActionBase
{
    public WorkingAtNight() : base()
    {
        this.actionName = "WorkingAtNight";
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
        if (CWorld.Instance.GetFacts().GetFact("nightShift").value != 1)
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