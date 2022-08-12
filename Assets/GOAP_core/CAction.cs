using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.World;
using Unity.GOAP.Agent;

namespace Unity.GOAP.ActionBase {
    public class CActionBase : Node
    {
        public int cost = 1;
        public string actionName = "New Action";
        public bool isInteruptable = true;

        public List<CFact> PreConditions;

        // The special effect used for planning only, does not affect the world state and the agent's state
        public List<CFact> Effects;

        // The effect to the agent's state, does not affect the world state
        public List<CFact> agentEffects;

        // The effect to the world state, does not affect the agent's state
        public List<CFact> worldEffects;

        [HideInInspector] public bool isActive = false;
        [HideInInspector] public bool forceReplan = false;

        [HideInInspector] public CFactManager preconditions;
        [HideInInspector] public CFactManager effects;

        protected CAgent agent;

        public CActionBase() : base()
        {
            this.actionName = this.GetType().Name;
        }

        public virtual void Initiate(CAgent a)
        {
            preconditions = new CFactManager();
            effects = new CFactManager();
            
            foreach (CFact f in PreConditions)
            {
                preconditions.AddFact(f.name, f.value);
            }

            foreach (CFact f2 in Effects)
            {
                effects.AddFact(f2.name, f2.value);
            }
            foreach (CFact f2 in agentEffects)
            {
                effects.AddFact(f2.name, f2.value);
            }
            foreach (CFact f2 in worldEffects)
            {
                effects.AddFact(f2.name, f2.value);
            }

            this.agent = a;
        }

        public virtual CActionBase Clone(CAgent a)
        {
            CActionBase clone = (CActionBase)this.MemberwiseClone();
            clone.agent = a;

            clone.preconditions = new CFactManager();
            clone.effects = new CFactManager();

            foreach (CFact f in PreConditions)
            {
                clone.preconditions.AddFact(f.name, f.value);
            }

            foreach (CFact f2 in Effects)
            {
                clone.effects.AddFact(f2.name, f2.value);
            }
            foreach (CFact f2 in agentEffects)
            {
                clone.effects.AddFact(f2.name, f2.value);
            }
            foreach (CFact f2 in worldEffects)
            {
                clone.effects.AddFact(f2.name, f2.value);
            }

            return clone;
        }

        // Check complete, required
        public virtual bool HasCompleted()
        {
            return true;
        }
        // Check if failed, required
        public virtual bool HasFailed()
        {
            return false;
        }

        // Main action, required
        public virtual bool PerformAction()
        {
            return false;
        }
        // Pre calculation if needed, return true to start performing the action.
        public virtual bool Pre_Perform()
        {
            return true;
        }
        // Pos calculation if needed.
        public virtual bool Pos_Perform()
        {
            Debug.Log("Complete performing: " + actionName);
            return true;
        }

        public virtual void OnFail()
        {
            Debug.Log("Agent: " + agent.agentName + " fail to perform performing: " + this.actionName);
            return;
        }

        [HideInInspector]public List<Node> childiren = new List<Node>();
    }
}
