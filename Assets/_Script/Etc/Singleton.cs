using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour 
    where T : MonoBehaviour
{
    static T instance;
    public static T Instance
    {
        get
        {
            // instance�� ���� �� ������ �˻��Ѵ�.
            if(instance == null)
            {
                instance = FindObjectOfType<T>(true);
            }

            return instance;
        }
    }

    private void Awake()
    {
        instance = GetComponent<T>();
    }
}
