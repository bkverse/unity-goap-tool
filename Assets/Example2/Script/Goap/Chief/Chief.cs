using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Agent;
using Unity.GOAP.World;
public class Chief : CAgent, IAgentExp2
{

    protected override void Start()
    {
        base.Start();
    }

    void IAgentExp2.IncSkipCounter()
    {
        Debug.LogError("this agent does not skip work");
    }
    void IAgentExp2.ResetBoredom()
    {
        Debug.Log("This Agent love his job");
    }
    void IAgentExp2.ResetHunger()
    {
        Debug.Log("This Agent never go hungry");
    }
    void IAgentExp2.EarnMoney(int amount)
    {
        Debug.Log("This Agent does not need to earn more money");
    }
    void IAgentExp2.ResetSkipCounter()
    {
        Debug.Log("This Agent does not skip work");
    }

    protected void Update()
    {
        if (currentGoal!=null && currentGoal.goalName.Contains("Play"))
        {
            if ((CWorld.Instance.GetFacts().GetFact("morningShift").value == 1) ||
                (CWorld.Instance.GetFacts().GetFact("afternoonShift").value == 1) ||
                ((CWorld.Instance.GetFacts().GetFact("eatingTime").value == 1)))
            {
                Debug.Log("So love my job so go to work now");
                this.InterruptCurrentAction();
            }
        }
    }
}
