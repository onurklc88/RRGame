using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPool : MonoBehaviour
{
    [Serializable]
    
    public struct Pool
    {
        public string name;
        public Queue<GameObject> pooledObjects;
        public GameObject objectPrefab;
        public int poolSize;
    }
    [SerializeField] private Pool[] pools = null;

    
    private void Awake()
    {
       for(int j = 0; j < pools.Length; j++)
        {
            pools[j].pooledObjects = new Queue<GameObject>();

            for (int i = 0; i < pools[j].poolSize; i++)
            {
                GameObject poolObject = Instantiate(pools[j].objectPrefab);
                poolObject.transform.SetParent(transform);
                poolObject.SetActive(false);
                pools[j].pooledObjects.Enqueue(poolObject);
            }
        }

       

    }
    public GameObject GetPooledObject(int objectType)
    {
        if(objectType >= pools.Length)
        {
            return null;
        }
        GameObject obj = pools[objectType].pooledObjects.Dequeue();
        obj.SetActive(true);
        pools[objectType].pooledObjects.Enqueue(obj);
        return obj;
    }
    
}
