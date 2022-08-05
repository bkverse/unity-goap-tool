using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.ActionBase;
using Unity.GOAP.Agent;
using UnityEngine.AI;
public class MoveToPlace : CActionBase
{
    private NavMeshAgent _navMeshAgent;
    [SerializeField] private string location;

    public MoveToPlace() : base()
    {
        this.actionName = "MoveToPlace";
    }

    public override bool Pre_Perform()
    {
        AllLocationInfor infor = AllLocationInfor.Instance;
        LocationInformation loc = infor.infos.Find(e => e.codeName.Equals(location));

        
        if (loc != null)
        {
            agent.position3D = loc.obj.transform.position;
            agent.agentFact.RemoveContains("atLoc");
            
            //agent.ShowVisual(true);

            return true;
        }

        var index = infor.infos.IndexOf(loc);
        agent.agentFact.ChangeFact("CurrentLocation", index);
        Debug.Log(index);

        Debug.LogError("Can not find position of the location: "+ location);
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
        if (!location.Contains("Park"))
        {
            //agent.ShowVisual(false);
        }
        return base.Pos_Perform();
    }
    public override bool HasCompleted()
    {
        if (_navMeshAgent.pathPending)
        {
            return false;
        }

        if ((_navMeshAgent.remainingDistance <= 0.5f))
        {

            return true;
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
