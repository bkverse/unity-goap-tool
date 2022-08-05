using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Unity.GOAP.ActionBase;
using Unity.GOAP.Agent;
public class ActionIdle : CActionBase
{
    public override bool Pre_Perform()
    {
        return true;
    }

    float timer = 0f;
    public override bool Pos_Perform()
    {
        timer = timer + Time.deltaTime;
        this.isActive = true;
        if (timer >= 1f)
        {
            Debug.Log("Complete performing: " + actionName);
            this.isActive = false;
            timer = 0;
        }

        return true;
    }
    public override bool PerformAction()
    {
        isActive = true;
        return true;
    }

    public override bool HasCompleted()
    {
        return true;
    }

    public override bool HasFailed()
    {
        return false;
    }


}
