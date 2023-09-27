using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool = ObjectPool.Pool;

public class DecalPool : MonoBehaviour
{
    private static List<Material> DECAL_MATERIALS = null;
    private const int NUMBER_OF_DECALS = 4;

    [SerializeField] private Pool[] _pools;
    public static Pool[] pools = null;
    public Pool[] _pool_ = null;

    private void OnEnable()
    {
        EventLibrary.ResetDecalObject.AddListener(ResetDecalObject);
    }
    private void OnDisable()
    {
        EventLibrary.ResetDecalObject.RemoveListener(ResetDecalObject);
    }

    private void Awake()
    {
        LoadMaterials();

        pools = _pools;
        for (int j = 0; j < pools.Length; j++)
        {
            ref Pool pool = ref (pools[j]);
            pool.PooledObjects = new Queue<GameObject>();

            for (int i = 0; i < pools[j].PoolSize; i++)
            {
                SpawnPrefab(ref pool);
            }
        }

        _pool_ = _pools;
    }

    private void LoadMaterials()
    {
        if (DECAL_MATERIALS == null)
        {
            DECAL_MATERIALS = new();
            string basePath = "Materials/Decals/";
            string baseMatName = "M_PaintParticleDecal";
            string path = basePath + baseMatName;
            for (int i = 0; i < NUMBER_OF_DECALS; i++)
            {
                string fullPath = path + $"_{i}";
                var mat = Resources.Load<Material>(fullPath);
                DECAL_MATERIALS.Add(mat);
            }
        }
    }

    private void SpawnPrefab(ref Pool pool)
    {
        GameObject poolObject = Instantiate(pool.ObjectPrefab);
        poolObject.transform.SetParent(transform);
        poolObject.GetComponent<Renderer>().material = DECAL_MATERIALS.RandomElement();
        poolObject.SetActive(false);
        pool.PooledObjects.Enqueue(poolObject);
    }

    public static GameObject GetPooledObject(int objectType)
    {
        if (objectType >= pools.Length)
        {
            return null;
        }
        GameObject obj = pools[objectType].PooledObjects.Dequeue();
        pools[objectType].PooledObjects.Enqueue(obj);
        return obj;
    }

    private void ResetDecalObject(GameObject obj)
    {
        obj.transform.position = Vector3.zero;
        obj.transform.SetParent(transform);
        obj.SetActive(false);

    }

}
