using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Agent;
using Unity.GOAP.ActionBase;
using Unity.GOAP.World;

public class ActionGoToWaitingArea : CActionBase
{
    // Start is called before the first frame update

    public override bool Pre_Perform()
    {
        GameObject target;
        target = GameObject.FindWithTag("WaitingArea");
        if (target == null)
        {
            return false;
        }
        agent.position3D = target.transform.position;

        return true;
    }

    public override bool PerformAction()
    {
        Patient patient = (Patient)agent;
        patient.navAgent.SetDestination(agent.position3D);
        isActive = true;

        return true;
    }

    public override bool Pos_Perform()
    {
        ResourceManager.Instance.AddPatient(agent);

        isActive = false;
        return true;
    }

    public override bool HasCompleted()
    {
        Patient patient = (Patient)agent;
        if (patient.navAgent.remainingDistance < 2f)
            return true;
        return false;
    }

    public override bool HasFailed()
    {
        Patient patient = (Patient)agent;
        if (HasCompleted())
        {
            return false;
        }

        if (patient.navAgent.enabled && !patient.navAgent.hasPath && !patient.navAgent.pathPending && patient.navAgent.remainingDistance == 0)
        {
            return true;
        }
        return false;
    }

}
