using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.GOAP.ActionBase;
using Unity.GOAP.Agent;
using Unity.GOAP.World;
public class Eating : CActionBase
{
    float timer;
    [SerializeField] float eatTime = 1f;

    public override bool Pre_Perform()
    {
        timer = 0;
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
        if (timer >= eatTime)
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
