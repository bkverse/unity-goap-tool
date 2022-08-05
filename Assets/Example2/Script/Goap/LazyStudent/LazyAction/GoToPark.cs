using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;
using Unity.GOAP.Agent;
using Unity.GOAP.ActionBase;

public class GoToPark : CActionBase
{
    private NavMeshAgent _navMeshAgent;
    IAgentExp2 student;
    public override bool Pre_Perform()
    {
        AllLocationInfor infor = AllLocationInfor.Instance;
        LocationInformation loc = infor.infos.Find(e => e.codeName.Equals("Park3"));
        student = (IAgentExp2)agent;

        if (loc != null)
        {
            agent.position3D = loc.obj.transform.position;
            agent.agentFact.RemoveContains("atLoc");
            //agent.ShowVisual(true);

            return true;
        }

        var index = infor.infos.IndexOf(loc);
        agent.agentFact.ChangeFact("CurrentLocation", index);

        Debug.LogError("Can not find position of the location: Park");
        return false;
    }

    public override bool PerformAction()
    {
        _navMeshAgent = agent.gameObject.GetComponent<NavMeshAgent>();
        _navMeshAgent.SetDestination(agent.position3D);
        isActive = true;
        return true;
    }

    public override bool Pos_Perform()
    {
        return base.Pos_Perform();
    }
    public override bool HasCompleted()
    {
        if (_navMeshAgent.pathPending)
        {
            return false;
        }

        if ((_navMeshAgent.remainingDistance <= 1f))
        {

            return true;
        }

        if ((agent.agentFact.GetFact("MorningStudyTime").value == 1) ||
            (agent.agentFact.GetFact("AfternoonStudyTime").value == 1))
        {
            student.IncSkipCounter();
        }


        return false;
    }

    public override bool HasFailed()
    {

        if (HasCompleted())
        {
            return false;
        }

        if (_navMeshAgent.enabled && !_navMeshAgent.hasPath && !_navMeshAgent.pathPending && _navMeshAgent.remainingDistance == 0)
        {
            return true;
        }
        return false;
    }

}
