using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.GOAP.Agent;
public sealed class ResourceManager
{
    private static readonly ResourceManager instance = new ResourceManager();

    static Queue<CAgent> patientQueue;
    private static Queue<GameObject> cubeQueue;


    private ResourceManager() { }

    public static ResourceManager Instance
    {
        get { return instance; }
    }
    static ResourceManager()
    {
        patientQueue = new Queue<CAgent>();

        cubeQueue = new Queue<GameObject>();
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cubicle");
        foreach (GameObject c in cubes)
        {
            cubeQueue.Enqueue(c);
        }
    }

    public void AddCube(GameObject c)
    {
        cubeQueue.Enqueue(c);
    }

    public GameObject RemoveCube()
    {
        if (cubeQueue.Count == 0)
        {
            return null;
        }
        return cubeQueue.Dequeue();
    }

    public void AddPatient(CAgent p)
    {
        patientQueue.Enqueue(p);
    }

    public CAgent RemovePatient()  
    {
        if (patientQueue.Count == 0)
        {
            return null;
        }
        return patientQueue.Dequeue();
    }
}
