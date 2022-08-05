using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "AllLocationInfor", menuName = "ScriptableObject/AllLocationInfor", order = 1)]
public class AllLocationInfor : ScriptableObject
{
    private static AllLocationInfor instance;
    public static AllLocationInfor Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<AllLocationInfor>("AllLocationInfor");
            }

            return instance;
        }
    }
    
    public List<LocationInformation> infos;
}
