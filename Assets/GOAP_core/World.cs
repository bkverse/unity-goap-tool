using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Unity.GOAP.World
{
    [System.Serializable]
    public class CFact
    {
        public string name;
        public float value;

        public CFact(string name, float value)
        {
            this.name = name;
            this.value = value;
        }

        // In case of changing this to another type of value, this may need to change
        public bool isEqual(CFact anotherFact)
        {
            return Equals(this.value, anotherFact.value);
        }
    }

    public class CFactManager
    {
        private List<CFact> facts;

        public CFactManager()
        {
            facts = new List<CFact>();
        }
        public CFactManager(List<CFact> factlist)
        {
            facts = new List<CFact>(factlist);
        }


        public List<CFact> GetFactList()
        {
            return facts;
        }

        public bool HasFact(CFact fact)
        {
            return facts.Any(fa => fa.name == fact.name);
        }

        public CFact GetFact(string name)
        {
            return facts.Find(fa => fa.name == name); 
        }

        public void ChangeFact(string name, float value)
        {
            CFact fact = GetFact(name);
            if (fact != null)
            {
                fact.value = value;
            }
            else
            {
                facts.Add(new CFact(name, value));
            }
        }
        public void AddFact(string name, float value)
        {
            if (GetFact(name) == null)
            {
                CFact fact = new CFact(name, value);
                facts.Add(fact);
            }
        }
        public void AddFact(CFact fact)
        {
            if (GetFact(fact.name)==null && fact!=null)
            {
                facts.Add(fact);
            }
        }

        public void RemoveFact(string name)
        {
            CFact fact = facts.Find(fa => fa.name == name);
            if (fact != null)
            {
                facts.Remove(fact);
            }
        }

        public void RemoveContains(string str)
        {
            foreach(CFact f in facts.ToList())
            {
                if (f.name.Contains(str))
                {
                    facts.Remove(f);
                }
            }
        }

        public void ClearFacts()
        {
            facts = new List<CFact>();
        }

        // Check if every facts in this list is existed in anotherSet's list
        public bool CompareFactList(CFactManager anotherSet)
        {
            foreach (CFact f in facts)
            {
                if (!anotherSet.HasFact(f))
                {
                    return false;
                }
                CFact f2 = anotherSet.GetFact(f.name);
                if (!f.isEqual(f2))
                {
                    return false;
                }
            }
            return true;
        }
    }

    public sealed class CWorld 
    {
        private static readonly CWorld instance = new CWorld();
        private static CFactManager factManager;

        static CWorld()
        {
            factManager = new CFactManager();
        }

        private CWorld() { }

        public static CWorld Instance
        {
            get { return instance; }
        }

        public CFactManager GetFacts()
        {
            return factManager;
        }

        public void PrintFact()
        {
            foreach (var VARIABLE in factManager.GetFactList())
            {
                Debug.LogError(VARIABLE.name +" " + VARIABLE.value);
            }
        }

    }

}
