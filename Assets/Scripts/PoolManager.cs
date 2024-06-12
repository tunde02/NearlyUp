using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PoolManager : Singleton<PoolManager>
{
    [Serializable]
    public class PoolInfo
    {
        public string ObjectName { get; set; }
        public int PoolSize { get; set; }
        public GameObject Prefab { get; set; }
    }


    [SerializeField] private Transform poolTransform;
    [SerializeField] private List<GameObject> objectPrefabList;
    [SerializeField] private List<Material> materialList;
    [SerializeField] private int poolSize = 600;


    private Queue<GameObject> objectPool;


    private void Start()
    {
        objectPool = new Queue<GameObject>();

        SceneManager.sceneLoaded += OnSceneLevel2Loaded;
    }

    private void OnSceneLevel2Loaded(Scene scene, LoadSceneMode mode)
    {
        if (!scene.name.Equals("Level 2"))
        {
            return;
        }

        InitializeObjectPool();
    }

    private void InitializeObjectPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefabList[UnityEngine.Random.Range(0, objectPrefabList.Count)], poolTransform);

            obj.GetComponent<Renderer>().material = materialList[UnityEngine.Random.Range(0, materialList.Count)];
            obj.SetActive(false);
            // objectPool.Enqueue(obj); // PoolableObject.OnDisable() 에서 이미 Enqueue해서 할 필요 없음
        }
    }

    public GameObject SpawnFromPool(Vector3 position, Quaternion rotation)
    {
        if (!objectPool.TryDequeue(out GameObject objectToSpawn))
        {
            objectToSpawn = CloneObject();
        }

        if (objectToSpawn != null)
        {
            objectToSpawn.transform.SetPositionAndRotation(position, rotation);
            objectToSpawn.SetActive(true);

            return objectToSpawn;
        }

        return null;
    }

    private GameObject CloneObject()
    {
        int randomNumber = UnityEngine.Random.Range(0, objectPrefabList.Count);
        GameObject obj = Instantiate(objectPrefabList[randomNumber], poolTransform);

        obj.SetActive(false);

        return obj;
    }

    public void ReturnToPool(GameObject obj)
    {
        objectPool.Enqueue(obj);
    }
}
