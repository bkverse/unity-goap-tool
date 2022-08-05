using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Agent;

public class LazyStudentSecVer : CAgent, IAgentExp2
{
    private int boredom = 0;
    private int hunger = 10;
    private int skipCounter = 0;


    [SerializeField] private int hungerIncPerSec = 20;
    [SerializeField] private int boredomIncPerSec = 20;
    
    [SerializeField] private float studyTimeMorning = 6;
    [SerializeField] private float studyTimeAfternoon = 13;
    [SerializeField] private float extraStudyTime = 17;

    Clock clock;

    public LazyStudentSecVer() : base()
    {
        this.agentName = "LazyStudentSecVer";
    }

    protected override void Start()
    {
        base.Start();
        
        clock = GameObject.FindObjectOfType<Clock>();
        UpdateFact(0);
        
        boredom = 0;
        hunger = 10;
        skipCounter = 0;

        this.UpdateGoalImportant("Ate", 10);
        this.UpdateGoalImportant("Played", 0);
    }
    void IAgentExp2.ResetBoredom()
    {
        this.boredom = 0;
        this.UpdateGoalImportant("Played", this.boredom);
    }

    void IAgentExp2.ResetHunger()
    {
        this.hunger = 0;
        this.UpdateGoalImportant("Ate", this.hunger);
    }

    void IAgentExp2.EarnMoney(int amount)
    {
        Debug.LogError("This agent does not earn money");
    }

    void IAgentExp2.ResetSkipCounter()
    {
        chkCounter = true;
        skipCounter = 0;
        this.agentFact.ChangeFact("requiredExtraStudied", 0);
        
        Debug.Log(skipCounter);
    }
    
    void IAgentExp2.IncSkipCounter()
    {
        if (chkCounter)
        {
            skipCounter += 1;
            chkCounter = false;
            Debug.Log("Student skip class " + skipCounter + " times, at " + hour + " o'clock");
        }
    }

    float timer = 0f;
    int hour;
    bool chkCounter = true;
    protected void UpdateFact(float t)
    {
        if (((hour + t) >= (studyTimeMorning)) && hour < 11)
        {
            this.agentFact.ChangeFact("MorningStudyTime", 1);
            this.agentFact.ChangeFact("FreeTime", 0);
        }
        else
        {
            this.agentFact.ChangeFact("MorningStudyTime", 0);

            if (((hour + t) >= (studyTimeAfternoon)) && hour < 17)
            {
                this.agentFact.ChangeFact("AfternoonStudyTime", 1);
                this.agentFact.ChangeFact("FreeTime", 0);
            }
            else
            {
                this.agentFact.ChangeFact("AfternoonStudyTime", 0);
                this.agentFact.ChangeFact("FreeTime", 1);
                if (((hour + t) >= (extraStudyTime)) && hour < 19)
                {
                    this.agentFact.ChangeFact("ExtraStudyTime", 1);
                }
                else
                {
                    this.agentFact.ChangeFact("ExtraStudyTime", 0);
                }
            }
        }


        if ((hour>=22) ||(hour < studyTimeMorning))
        {
            this.agentFact.ChangeFact("SleepTime", 1);
            chkCounter = true;
        }
        else
        {
            this.agentFact.ChangeFact("SleepTime", 0);
        }

        if ((((hour>=11) && (hour< studyTimeAfternoon))) || ((hour >= 19) && (hour<21)))
        {
            this.agentFact.ChangeFact("EatTime", 1);
            this.agentFact.ChangeFact("FreeTime", 1);
            chkCounter = true;
        }
        else
        {
            this.agentFact.ChangeFact("EatTime", 0);
        }

        if (skipCounter>=3)
        {
            this.agentFact.ChangeFact("requiredExtraStudied", 1);
        }
        else
        {
            this.agentFact.ChangeFact("requiredExtraStudied", 0);
        }
    }

    protected void Update()
    {
        hour = clock.GetHour();
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            timer = 0;

            UpdateFact(timer);
            if (currentAction != null)
            {
                if (currentAction.name.Contains("Studying"))
                {
                    this.hunger += this.hungerIncPerSec;
                    this.boredom += this.boredomIncPerSec;
                }

                if (currentAction.name.Contains("Playing"))
                {
                    this.hunger += this.hungerIncPerSec;
                    this.boredom -= this.boredomIncPerSec;
                    if (this.boredom <= 0)
                    {
                        this.boredom = 0;
                    }
                }

                this.UpdateGoalImportant("Ate", this.hunger);
                this.UpdateGoalImportant("Played", this.boredom);

                if (hunger >= 110 && !currentGoal.goalName.Contains("Ate"))
                {
                    Debug.Log("Too hungry to do anything");
                    this.InterruptCurrentAction();
                }
                else
                {
                    if (boredom >= 110 && !currentGoal.goalName.Contains("Play"))
                    {
                        Debug.Log("Too bored, got to go play");
                        //if (currentGoal.goalName.Equals("Studied") )
                        //{
                        //    this.skipCounter += 1;
                        //}
                        Debug.Log(hour);
                        this.InterruptCurrentAction();
                    }
                }

                if (!currentGoal.goalName.Contains("Studied") && currentGoal.important < 100)
                {
                    if ((agentFact.GetFact("ExtraStudyTime").value == 1) &&
                        (agentFact.GetFact("requiredExtraStudied").value == 1))
                    {
                        Debug.Log("Can not go play right now");
                        this.InterruptCurrentAction();
                    }
                }
            }

        }

    }
}
