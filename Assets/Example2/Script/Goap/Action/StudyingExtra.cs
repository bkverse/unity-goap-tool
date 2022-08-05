using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.GOAP.ActionBase;
using Unity.GOAP.Agent;
using UnityEngine.AI;
using Unity.GOAP.World;

    public class StudyingExtra : CActionBase
    {
    float timer;
    [SerializeField] float studyTime = 1;
    public override bool Pre_Perform()
    {
        timer = 0;
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
        timer += Time.deltaTime;

        if (timer >= studyTime)
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