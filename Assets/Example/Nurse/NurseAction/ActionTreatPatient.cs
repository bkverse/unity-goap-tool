using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.ActionBase;
using Unity.GOAP.Agent;

public class ActionTreatPatient : CActionBase
{
    GameObject cube;

    public override bool Pre_Perform()
    {
        Nurse nurse = (Nurse)agent;
        foreach (GameObject go in nurse.inventory)
        {
            if (go.tag == "Cubicle")
            {
                cube = go;
                break;
            }
        }

        if (cube == null)
        {
            return false;
        }

        agent.position3D = cube.transform.position;
        return true;
    }

    float timer = 0f;
    public override bool Pos_Perform()
    {
        timer = timer + Time.deltaTime;
        this.isActive = true;
        if (timer >= 2f)
        {
            Debug.Log("Complete performing: " + actionName);
            this.isActive = false;
            ResourceManager.Instance.AddCube(cube);
            Nurse nurse = (Nurse)agent;
            nurse.inventory.Remove(cube);
            timer = 0;
        }
        return true;
    }
    public override bool PerformAction()
    {
        Nurse nurse = (Nurse)agent;
        nurse.navAgent.SetDestination(agent.position3D);
        isActive = true;
        return true;
    }
    public override bool HasCompleted()
    {
        Nurse nurse = (Nurse)agent;
        if (nurse.navAgent.remainingDistance < 2f)
            return true;
        return false;
    }

    public override bool HasFailed()
    {
        if (HasCompleted())
        {
            return false;
        }
        Nurse nurse = (Nurse)agent;

        if (nurse.navAgent.enabled && !nurse.navAgent.hasPath && !nurse.navAgent.pathPending && nurse.navAgent.remainingDistance == 0)
        {
            return true;
        }
        return false;
    }
}
