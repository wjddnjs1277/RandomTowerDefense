using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPools : Singleton<ObjectPools>
{
    [SerializeField] GameObject[] effectPrefab;
    [SerializeField] Transform storagePerent;
    [SerializeField] int initCount;

    Dictionary<string, GameObject> prefabs;
    Dictionary<string, Queue<GameObject>> storage;

    private void Awake()
    {
        prefabs = new Dictionary<string, GameObject>();
        for(int i =0; i <effectPrefab.Length; i++)
        {
            string name = effectPrefab[i].name;
            prefabs.Add(name, effectPrefab[i]);
        }

        storage = new Dictionary<string, Queue<GameObject>>();
        foreach(string key in prefabs.Keys)
        {
            storage.Add(key, new Queue<GameObject>());
            for (int i = 0; i < initCount; i++)
                Create(key);
        }
    }

    void Create(string key)
    {
        GameObject obj = Instantiate(prefabs[key], storagePerent);
        obj.gameObject.name = key;
        obj.SetActive(false);
        storage[key].Enqueue(obj);
    }

    public void ReturnPool(GameObject obj)
    {
        string key = obj.gameObject.name;
        storage[key].Enqueue(obj);
        obj.transform.SetParent(storagePerent);
        obj.SetActive(false);
    }

    public GameObject GetPool(string key)
    {
        if (storage[key].Count <= 0)
            Create(key);

        GameObject obj = storage[key].Dequeue();
        obj.transform.SetParent(transform);
        obj.SetActive(true);

        return obj;
    }
}
