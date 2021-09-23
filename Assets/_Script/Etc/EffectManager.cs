using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : ObjectPools
{
    public void ShowEffect(string effectName, Vector3 pos)  
    {
        GameObject obj = GetPool(effectName);
        obj.transform.position = pos;
    }
}
