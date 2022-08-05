using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Agent;
using Unity.GOAP.World;
public class WorkerAgent : CAgent, IAgentExp2
{
    float timer;

    int money;
    int hunger;

    [SerializeField] private int hungerIncPerSec = 10;
    [SerializeField] private int moneyDecPerSec = 20;
    [SerializeField] private int moneyIncPerShift = 50;


    protected override void Start()
    {
        base.Start();

        money = 100;
        hunger = 0;
        timer = 0;

        this.UpdateGoalImportant("Eat", this.hunger);
        this.UpdateGoalImportant("WorkNightShift", 50 - this.money);
        this.UpdateGoalImportant("WorkExtraHours", 100 - this.money);
    }

    void IAgentExp2.EarnMoney(int extra)
    {
        this.money += moneyIncPerShift + extra;
    }

    void IAgentExp2.IncSkipCounter()
    {
        Debug.LogError("this agent does not skip work");
    }
    void IAgentExp2.ResetBoredom()
    {
        Debug.Log("This agent does not have a soul to enjoy life anymore");
    }

    void IAgentExp2.ResetHunger()
    {
        this.hunger = 0;
        this.UpdateGoalImportant("Eat", this.hunger);
    }

    void IAgentExp2.ResetSkipCounter()
    {
        Debug.LogError("this agent does not skip work");
    }

    protected void UpdateFact()
    {
        if (hunger >= 60)
        {
            this.agentFact.ChangeFact("isHungry",1);
        }
        else
        {
            this.agentFact.ChangeFact("isHungry", 0);
        }

        if (money <=0)
        {
            this.agentFact.ChangeFact("needMoney", 1);
        }
        else
        {
            this.agentFact.ChangeFact("needMoney", 0);
        }
    }

    protected void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            timer = 0;
            UpdateFact();
            if (currentAction != null)
            {
                if (currentAction.actionName.Contains("Work"))
                {
                    this.hunger += this.hungerIncPerSec;
                }

                if (currentAction.actionName.Contains("Playing"))
                {
                    this.hunger += this.hungerIncPerSec;
                    this.money -= this.moneyDecPerSec;
                }

                this.UpdateGoalImportant("Eat", this.hunger);
                this.UpdateGoalImportant("WorkNightShift", 50 - this.money);
                this.UpdateGoalImportant("WorkExtraHours", 100 - this.money);

                if (hunger >= 110 && !currentGoal.goalName.Contains("Eat"))
                {
                    Debug.Log("Too hungry to do anything");
                    this.InterruptCurrentAction();
                }

                if (currentGoal.goalName.Contains("Play") && !currentGoal.goalName.Contains("Eat"))
                {
                    if ((CWorld.Instance.GetFacts().GetFact("morningShift").value == 1) || 
                        (CWorld.Instance.GetFacts().GetFact("afternoonShift").value == 1))
                    {
                        Debug.Log("Can not go play right now");
                        this.InterruptCurrentAction();
                    }
                }

            }

        }
    }
}
