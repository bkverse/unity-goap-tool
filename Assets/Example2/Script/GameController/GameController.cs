using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
   public static GameController Instance;

   private void Awake()
   {
      if (Instance != null && Instance != this)
      {
         Destroy(this);
      }
      else
      {
         Instance = this;
      }
   }

   private void Start()
   {
      DontDestroyOnLoad(this);
      SpawnPeople(totalPeople);
   }

   [SerializeField] private List<GameObject> people;
   [SerializeField] private int totalPeople;
   [SerializeField] private List<GameObject> corners;
   [SerializeField] private Transform botPool;
   
   private GameObject SpawnPerson(GameObject prefab)
   {
      float posZ = Random.Range(corners[1].transform.position.z, corners[0].transform.position.z);
      float posX = Random.Range(corners[2].transform.position.x, corners[0].transform.position.x);
      float posY = corners[0].transform.position.y;

      GameObject obj = SimplePool.Spawn(prefab, new Vector3(posX, posY, posZ), Quaternion.identity);
      obj.transform.SetParent(botPool);
      return obj;
   }


   private void SpawnPeople(int total)
   {
      for (int i = 0; i < total; i++)
      {
         int id = Random.Range(0, people.Count);
         GameObject obj = SpawnPerson(people[id]);
         if (obj == null)
         {
            i -= 1;
         }
      }
      CameraController.Instance.GetAllCam();
   }
   
}
