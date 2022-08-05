using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.GOAP.ActionBase;
using Unity.GOAP.Agent;
using UnityEngine.AI;
using Unity.GOAP.World;

    public class Studying : CActionBase
    {
    [SerializeField] bool isMorningStudy;

    public override bool Pre_Perform()
    {
        return true;
    }

    public override bool PerformAction()
    {
        return true;
    }


    public override bool Pos_Perform()
    {
        return true;
    }
    public override bool HasCompleted()
    {
        if (isMorningStudy)
        {
            if (CWorld.Instance.GetFacts().GetFact("morningShift").value!=1)
            {
                return true;
            }
        }
        else
        {
            if (CWorld.Instance.GetFacts().GetFact("afternoonShift").value!=1)
            {
                return true;
            }
        }
        return false;
    }

    public override bool HasFailed()
    {
        return false;
    }

}