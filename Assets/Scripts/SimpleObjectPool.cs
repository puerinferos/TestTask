using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleObjectPool
{
    private Queue<MonoPoolable> _poolObjects;

    private List<MonoPoolable> _prefabs;

    public SimpleObjectPool(int count, List<MonoPoolable> prefabs)
    {
        _poolObjects = new Queue<MonoPoolable>(count);

        _prefabs = prefabs;

        for (int i = 0; i < count; i++)
        {
            MonoPoolable prefab = _prefabs[Random.Range(0, _prefabs.Count)];
            MonoPoolable prefabClone = GameObject.Instantiate(prefab);
            prefabClone.PoolInitialize(this);
            _poolObjects.Enqueue(prefabClone);
            prefabClone.gameObject.SetActive(false);
        }
    }

    public MonoPoolable PoolInstantiate(Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (_poolObjects.Count > 0)
        {
            MonoPoolable poolObject = _poolObjects.Dequeue();
            poolObject.transform.SetParent(parent);
            poolObject.transform.localPosition = position;
            poolObject.transform.rotation = rotation;
            poolObject.gameObject.SetActive(true);

            return poolObject;
        }

        MonoPoolable prefab = _prefabs[Random.Range(0, _prefabs.Count)];
        MonoPoolable prefabClone = GameObject.Instantiate(prefab, parent, true);
        prefabClone.transform.localPosition = position;
        prefabClone.transform.rotation = rotation;
        
        prefabClone.PoolInitialize(this);
        return prefabClone;
    }

    public void ReturnToPool(MonoPoolable itemToReturn)
    {
        itemToReturn.gameObject.SetActive(false);
        _poolObjects.Enqueue(itemToReturn);
    }
}