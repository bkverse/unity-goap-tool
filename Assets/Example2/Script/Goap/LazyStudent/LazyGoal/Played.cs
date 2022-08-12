using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Goal;
using Unity.GOAP.Agent;

public class Played : CGoal
{
    LazyStudentSecVer student;
    public override void OnComplete()
    {
        base.OnComplete();
        student = (LazyStudentSecVer)agent;
    }
}
