using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.ActionBase;
using Unity.GOAP.Agent;


public class ActionGettingTreat : CActionBase
{
    public override bool Pre_Perform()
    {
        GameObject target = null;
        Patient p = (Patient)agent;
        foreach (GameObject go in p.inventory)
        {
            if (go.tag == "Cubicle")
            {
                target = go;
                break;
            }
        }

        if (target == null)
        {
            return false;
        }

        agent.position3D = target.transform.position;
        return true;
    }

    float timer = 0f;
    public override bool Pos_Perform()
    {
        timer += Time.deltaTime;
        this.isActive = true;
        if (timer >= 5f)
        {
            Debug.Log("Complete performing: " + actionName);
            this.isActive = false;
            timer = 0;
        }
        return true;
    }

    public override bool PerformAction()
    {
        Patient patient = (Patient)agent;
        patient.navAgent.SetDestination(agent.position3D);
        isActive = true;
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
        if (HasCompleted())
        {
            return false;
        }

        Patient patient = (Patient)agent;
        if (patient.navAgent.enabled && !patient.navAgent.hasPath && !patient.navAgent.pathPending && patient.navAgent.remainingDistance == 0)
        {
            return true;
        }
        return false;
    }
}
