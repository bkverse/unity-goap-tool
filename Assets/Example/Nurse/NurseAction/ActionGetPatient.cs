using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.ActionBase;
using Unity.GOAP.Agent;

public class ActionGetPatient : CActionBase
{
    Patient patient;
    GameObject cube;
    public override bool Pre_Perform()
    {
        cube = ResourceManager.Instance.RemoveCube();
        if (cube == null)
        {
            return false;
        }

        patient = (Patient) ResourceManager.Instance.RemovePatient();
        if (patient == null)
        {
            ResourceManager.Instance.AddCube(cube);
            return false;
        }

        agent.position3D = patient.transform.position;
        return true;
    }

    public override bool Pos_Perform()
    {
        patient.inventory.Add(cube);

        Nurse nurse = (Nurse)agent;
        nurse.inventory.Add(cube);

        return base.Pos_Perform();
    }

    public override bool HasCompleted()
    {
        Nurse nurse = (Nurse)agent;
        if (nurse.navAgent.remainingDistance < 1f)
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

    public override bool PerformAction()
    {
        Nurse nurse = (Nurse)agent;
        nurse.navAgent.SetDestination(agent.position3D);
        isActive = true;

        return true;
    }

}
