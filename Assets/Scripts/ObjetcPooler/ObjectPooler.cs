using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : Singleton<ObjectPooler>
{
    [SerializeField]
    private List<PoolItemData> _poolableList = new List<PoolItemData>();
    private Dictionary<PoolObjectType, Queue<GameObject>> _objectPool;

    private void Start()
    {
        _objectPool = new Dictionary<PoolObjectType, Queue<GameObject>>();
        foreach (PoolItemData item in _poolableList)
        {
            Queue<GameObject> objectQue = new Queue<GameObject>();
            for (int i = 0; i < item.quantity; i++)
            {
                GameObject gmo = Instantiate(item.objectPrefab);
                gmo.SetActive(false);
                objectQue.Enqueue(gmo);
            }

            _objectPool.Add(item.objectType, objectQue);
        }
    }

    public GameObject SpawnFromPool(PoolObjectType objectType, Vector3 spawnPosition, Quaternion rotation)
    {

        GameObject spawnedGMO = _objectPool[objectType].Dequeue();
        spawnedGMO.SetActive(true);
        spawnedGMO.transform.position = spawnPosition;
        spawnedGMO.transform.rotation = rotation;

        return spawnedGMO;
    }

    public void PoolDestroy(PoolObjectType objectType, GameObject objectToDestroy) 
    {
        objectToDestroy.SetActive(false);
        _objectPool[objectType].Enqueue(objectToDestroy);
    }

}

[System.Serializable]
public class PoolItemData
{
    public PoolObjectType objectType;
    public GameObject objectPrefab;
    public int quantity;
}

public enum PoolObjectType
{
    bomb,
    heal,
    coin
}