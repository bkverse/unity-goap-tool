using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using Unity.GOAP.World;
using Unity.GOAP.ActionBase;
using Unity.GOAP.Goal;
using Unity.GOAP.Planner;

namespace Unity.GOAP.Agent
{
    public class CAgent : MonoBehaviour
    {
        public string agentName = "agent Name";

        // Agent own belief system, or state that only the owner has access to.
        // This opposited to the world states, where all agent get access.
        public CFactManager agentFact;

        [HideInInspector] public List<CActionBase> actionList;
        [HideInInspector] public List<CGoal> goalList;
        [HideInInspector] public List<CGoal> goalBlacklist;

        [HideInInspector] public Vector3 position3D;

        protected Queue<CActionBase> actionQueue;
        protected CActionBase currentAction;
        protected CGoal currentGoal;

        [SerializeField] protected AgentView agentView;

        protected CPlanner planner;

        public CAgent() : base()
        {
            this.agentName = this.GetType().Name;
        }

        protected virtual void Awake()
        {
            agentFact = new CFactManager();

            actionList = new List<CActionBase>();
            goalList = new List<CGoal>();

            foreach (CActionBase a in agentView.actions)
            {
                a.Initiate(this);
                CActionBase act =  a.Clone(this);
                actionList.Add(act);
            }

            foreach (CGoal g in agentView.goals)
            {
                g.Initiate(this);
                CGoal goal = g.Clone(this);
                goalList.Add(goal);
            }
        }

        protected virtual void Start()
        {

        }

        // function for dynamic control over action/goal
        public void AddAction(CActionBase action)
        {
            if (!this.actionList.Contains(action))
            {
                this.actionList.Add(action);
            }
        }
        public void AddGoal(CGoal goal)
        {
            if (!this.goalList.Contains(goal))
            {
                this.goalList.Add(goal);
            }
        }

        public void UpdateActionCost(string aName, int newCost)
        {
            var act =  this.actionList.Find(a => a.actionName == aName);
            if (act != null)
            {
                act.cost = newCost;
            }
        }

        public void UpdateGoalImportant(string gName, int newImportant)
        {
            var go = this.goalList.Find(g => g.goalName == gName);
            if (go != null)
            {
                go.important = newImportant;
            }
            else
            {
                var go2 = this.goalBlacklist.Find(g => g.goalName == gName);
                if (go2 != null)
                {
                    go2.important = newImportant;
                }
            }
        }

        protected void InterruptCurrentAction()
        {
            if (currentAction != null)
            {
                if (currentAction.isInteruptable)
                {
                    goalList.Remove(currentGoal);
                    BlackListingGoal(currentGoal);
                    this.GetAGoal();
                }
            }
        }


        // Called after t time to reset blacklist back to goal list
        protected void ResetBlackList()
        {
            foreach (CGoal g in goalBlacklist.ToList())
            {
                if (!goalList.Contains(g))
                {
                    goalList.Add(g);
                }
                goalBlacklist.Remove(g);
            }
        }

        protected void BlackListingGoal(CGoal goal)
        {
            if (!goalBlacklist.Contains(goal) && goal != null)
            {
                goalBlacklist.Add(goal);
            }
        }


        // Function to create a new plan (action queue)
        protected virtual void GetAGoal()
        {
            planner = new CPlanner();
            currentGoal = null;
            currentAction = null;
            var sortedGoal = goalList.OrderByDescending(g => g.important);

            foreach (CGoal g in sortedGoal)
            {
                List<CActionBase> alist = new List<CActionBase>(actionList);
                actionQueue = planner.Plan(g, alist, agentFact);
                if (actionQueue != null)
                {
                    currentGoal = g;
                    currentGoal.OnStart();
                    break;
                }
            }

        }


        protected virtual void PerformAction(CActionBase action) 
        {
            // If the action is performable by checking Pre_performing calculation, default always return true
            if (currentAction.Pre_Perform())
            {
                Debug.Log("Agent: " + agentName + " currently performing: " + currentAction.actionName);
                currentAction.isActive = true;
                currentAction.PerformAction();
            }
            // If checking Pre_performing false, meaning the action is unable to perform for some reason, temporary remove 
            // the goal and re-plan.
            else
            {
                currentAction.OnFail();
                goalList.Remove(currentGoal);
                BlackListingGoal(currentGoal);
                GetAGoal();
            }
            return;
        }

        float goalResetTimer = 0f;
        protected virtual void LateUpdate()
        {
            // After every x sec, reset the blacklist, this can be changed to different counter methods
            goalResetTimer = goalResetTimer += Time.deltaTime;
            if (goalResetTimer >= 2f)
            {
                ResetBlackList();
                goalResetTimer = 0;
            }


            // Check if currently running any action
            if ((currentAction != null) && (currentAction.isActive))
            {
                // If durring perfoming action, things happen that cause the action to fail, temporary remove 
                // the goal and re-plan.
                if (currentAction.HasFailed())
                {
                    currentAction.OnFail();

                    foreach (CGoal g in goalList)
                    {
                        Debug.Log("Check GoalList before: " + g.goalName);
                    }

                    goalList.Remove(currentGoal);

                    foreach (CGoal g in goalList)
                    {
                        Debug.Log("Check GoalList after: " + g.goalName);
                    }

                    BlackListingGoal(currentGoal);
                    GetAGoal();
                    return;
                }

                // Check if the current action has complete yet?
                if (currentAction.HasCompleted())
                {
                    // Do pos calculation and switch to the next action
                    currentAction.isActive = false;
                    currentAction.Pos_Perform();
                    if (currentAction.forceReplan == true)
                    {
                        GetAGoal();
                        return;
                    }

                    // Should performing the action automatically add effect to the world state?
                    // If should, what will it be? World state or belief?
                    // As being undecided right now, the effect will be splited into 3

                    // Adding 
                    foreach (CFact f2 in currentAction.worldEffects)
                    {
                        CWorld.Instance.GetFacts().AddFact(f2);
                    }

                    foreach (CFact f2 in currentAction.agentEffects)
                    {
                        agentFact.AddFact(f2);
                    }
                    return;
                }
            }
            else
            {
                // If there is a goal to pursuit
                if (currentGoal != null)
                {
                    // Check if done all action is finished. For we planed the action sequence to satisfied the goal, if the 
                    // action sequence is complete, most likely the goal will be satisfied. therefore this should count as 
                    // checking the completation of the goal.
                    if (actionQueue.Count <= 0)
                    {
                        currentGoal.OnComplete();
                        // If goal is satisfied and non repeat, remove from the goal list
                        if (currentGoal.deletable)
                        {
                            goalList.Remove(currentGoal);
                        }
                        // And then find a new goal
                        GetAGoal();
                        return;
                    }
                    else
                    {
                        // Take 1 action from the plan queue, and execute it.
                        currentAction = actionQueue.Dequeue();
                        PerformAction(currentAction);
                        // Action is completed then remove the action from the action queue
                        return;
                    }
                }
                else
                {
                    GetAGoal();
                    return;
                }
            }

        }
    }

}