using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Agent;
using Unity.GOAP.World;
public class HardWorkingStudent : CAgent, IAgentExp2
{
    float timer;
    int hunger;
    int boredom;

    [SerializeField] private int hungerIncPerSec = 10;
    [SerializeField] private int boredomIncPerSec = 10;

    protected override void Start()
    {
        base.Start();

        hunger = 0;
        boredom = 0;
        timer = 0;

        this.UpdateGoalImportant("Eat", this.hunger);
        this.UpdateGoalImportant("PlayAtThePark", this.boredom);
    }

    void IAgentExp2.IncSkipCounter()
    {
        Debug.LogError("this agent does not skip work");
    }
    void IAgentExp2.ResetBoredom()
    {
        boredom = 0;
        this.UpdateGoalImportant("PlayAtThePark", this.boredom);
    }
    void IAgentExp2.ResetHunger()
    {
        hunger = 0;
        this.UpdateGoalImportant("Eat", this.hunger);
    }
    void IAgentExp2.EarnMoney(int amount)
    {
        Debug.Log("This Agent does not earn money");
    }
    void IAgentExp2.ResetSkipCounter()
    {
        Debug.Log("This Agent does not skip school");
    }

    protected void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            timer = 0;
            if (currentAction != null)
            {
                if (currentAction.actionName.Contains("Study"))
                {
                    this.hunger += this.hungerIncPerSec;
                    this.boredom += this.boredomIncPerSec;
                }

                if (currentAction.actionName.Contains("Play"))
                {
                    this.hunger += this.hungerIncPerSec;
                    this.boredom -= this.boredomIncPerSec*10;

                    if (this.boredom <= 0)
                        boredom = 0;
                }

                this.UpdateGoalImportant("Eat", this.hunger);
                this.UpdateGoalImportant("PlayAtThePark", this.boredom);

                if (hunger >= 110 && !currentGoal.goalName.Contains("Eat"))
                {
                    Debug.Log("Too hungry to do anything");
                    this.InterruptCurrentAction();
                }

                if (!currentGoal.goalName.Equals("Study") && currentGoal.important<100)
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
