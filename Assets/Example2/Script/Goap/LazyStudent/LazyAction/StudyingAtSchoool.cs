using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Agent;
using Unity.GOAP.ActionBase;

public class StudyingAtSchoool : CActionBase
{
    [SerializeField] bool isMorningStudy=true;
    public override bool Pre_Perform()
    {
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
        if (isMorningStudy)
        {
            if (agent.agentFact.GetFact("MorningStudyTime").value != 1)
            {
                return true;
            }
        }
        else
        {
            if (agent.agentFact.GetFact("AfternoonStudyTime").value != 1)
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
