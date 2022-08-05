using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.ActionBase;
public class StudyingExtraClass : CActionBase
{
    public override bool HasCompleted()
    {
        if (agent.agentFact.GetFact("ExtraStudyTime").value != 1)
        {
            return true;
        }

        return false;
    }
}
