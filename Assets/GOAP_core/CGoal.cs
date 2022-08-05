using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.World;
using Unity.GOAP.Agent;

namespace Unity.GOAP.Goal
{
    public class CGoal : Node
    {
        public int important = 1;
        public string goalName = "SomeGoal";
        public bool deletable = true;
        
        public List<CFact> goalList;

        public CFactManager goals;

        protected CAgent agent;

        public CGoal() : base()
        {
            this.goalName = this.GetType().Name;
        }

        public virtual void Initiate(CAgent a)
        {
            goals = new CFactManager(goalList);
            this.agent = a;
        }

        public virtual CGoal Clone(CAgent a)
        {
            CGoal clone = (CGoal)MemberwiseClone();
            clone.agent = a;
            clone.goals = new CFactManager(goalList);
            return clone;
        }

        // Function called on starting the goal
        public virtual void OnStart()
        {
            Debug.Log("Agent: " + agent.agentName + " start for goal: " + this.goalName);
            return;
        }

        // Function called on completing the goal
        public virtual void OnComplete()
        {
            Debug.Log("Agent: " + agent.agentName + " complete Goal: " + this.goalName);
            return;
        }

        // Function to check if goal is satisfied
        public virtual bool IsSatified(CFactManager curState)
        {
            //if (curState.CompareFactList(goals))
            if (goals.CompareFactList(curState))
                {
                    return true;
            }
            return false;
        }

    }
}
