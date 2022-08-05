using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Agent;
using Unity.GOAP.World;
public class Policeman : CAgent, IAgentExp2
{
    float timer;

    int hunger;

    [SerializeField] private int hungerIncPerSec = 10;

    protected override void Start()
    {
        base.Start();
        timer = 0;
        this.UpdateGoalImportant("Eat", this.hunger);
    }

    void IAgentExp2.EarnMoney(int extra)
    {
        Debug.Log("this agent's job pay so well he loves his job");
    }


    void IAgentExp2.ResetBoredom()
    {
        Debug.Log("this agent love his job so don not get bored");
    }

    void IAgentExp2.ResetHunger()
    {
        Debug.Log("reset hunger");
        this.hunger = 0;
        this.UpdateGoalImportant("Eat", this.hunger);
    }

    void IAgentExp2.ResetSkipCounter()
    {
        Debug.LogError("this agent does not skip work");
    }
    void IAgentExp2.IncSkipCounter()
    {
        Debug.LogError("this agent does not skip work");
    }

    protected void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            timer = 0;
            if (currentAction != null)
            {
                if (currentAction.actionName.Contains("Work"))
                {
                    this.hunger += this.hungerIncPerSec;
                }

                if (currentAction.actionName.Contains("Playing"))
                {
                    this.hunger += this.hungerIncPerSec;
                }

                this.UpdateGoalImportant("Eat", this.hunger);

                if (hunger >= 110 && !currentGoal.goalName.Contains("Eat"))
                {
                    Debug.Log("Too hungry to do anything");
                    this.InterruptCurrentAction();
                }
                else
                {
                    if (!currentGoal.goalName.Contains("Work") && !currentGoal.goalName.Contains("Eat"))
                    {
                        if ((CWorld.Instance.GetFacts().GetFact("morningShift").value == 1) ||
                            (CWorld.Instance.GetFacts().GetFact("afternoonShift").value == 1) ||
                            (CWorld.Instance.GetFacts().GetFact("nightShift").value == 1))
                        {
                            Debug.Log("So love my job so go to work now");
                            this.InterruptCurrentAction();
                        }
                    }
                }


            }

        }
    }
}
