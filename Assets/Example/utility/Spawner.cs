using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject patientPrefab;
    public int numPatients;

    public int maxPatients;
    int countPatients;

    void Start()
    {
        for (int i = 0; i < numPatients; i++)
        {
            Instantiate(patientPrefab, this.transform.position, Quaternion.identity);
            countPatients++;
        }
        Invoke("SpawnPatients", 5);
    }

    void SpawnPatients()
    {
        if (countPatients < maxPatients)
        {
            Instantiate(patientPrefab, this.transform.position, Quaternion.identity);
            Invoke("SpawnPatients", Random.Range(2, 10));
            countPatients++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
