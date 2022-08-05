using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Agent;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Patient : CAgent
{
    [HideInInspector] public List<GameObject> inventory;
    [HideInInspector] public NavMeshAgent navAgent;

    protected override void Awake()
    {
        base.Awake();
        navAgent = this.gameObject.GetComponent<NavMeshAgent>();
    }

    protected override void Start()
    {
        base.Start();

    }

    public void Move()
    {
        navAgent.SetDestination(this.position3D);
    }

    public void Move(Vector3 location)
    {
        navAgent.SetDestination(location);
    }
}
