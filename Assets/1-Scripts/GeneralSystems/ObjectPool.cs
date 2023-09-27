using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPool : MonoBehaviour
{
    [Serializable]
    
    public class Pool
    {
        public string Name;
        public Queue<GameObject> PooledObjects;
        public GameObject ObjectPrefab;
        public int PoolSize;
    }

    [SerializeField] private Pool[] _pools;
    public static Pool[] pools = null;

    private void OnEnable()
    {
        EventLibrary.ResetPooledObject.AddListener(ResetPooledObject);
    }
    private void OnDisable()
    {
        EventLibrary.ResetPooledObject.RemoveListener(ResetPooledObject);
    }

    private void Awake()
    {
        pools = _pools;
       for(int j = 0; j < pools.Length; j++)
        {
            pools[j].PooledObjects = new Queue<GameObject>();

            for (int i = 0; i < pools[j].PoolSize; i++)
            {
                GameObject poolObject = Instantiate(pools[j].ObjectPrefab);
                poolObject.transform.SetParent(transform);
                poolObject.SetActive(false);
                pools[j].PooledObjects.Enqueue(poolObject);
            }
        }
    }

    public static GameObject GetPooledObject(int objectType)
    {
        if(objectType >= pools.Length)
        {
            return null;
        }
        GameObject obj = pools[objectType].PooledObjects.Dequeue();
        pools[objectType].PooledObjects.Enqueue(obj);
        return obj;
    }

    private void ResetPooledObject(GameObject obj)
    {
        obj.transform.position = Vector3.zero;
        obj.transform.SetParent(transform);
        obj.SetActive(false);
      
    }

   
}
