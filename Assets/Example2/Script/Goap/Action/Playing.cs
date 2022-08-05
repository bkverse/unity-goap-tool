using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.GOAP.ActionBase;
using Unity.GOAP.Agent;
public class Playing : CActionBase
{
    float timer = 0f;
    [SerializeField] float playTime = 10f;
    public Playing() : base()
    {
        this.actionName = "Playing";
    }
    public override bool Pre_Perform()
    {
        timer = 0f;
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
