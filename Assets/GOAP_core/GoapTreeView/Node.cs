using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node : ScriptableObject
{
   public enum State
   {
      Idle,
      Running
   }

   [HideInInspector]public string guid;
   [HideInInspector]public Vector2 position;
}
