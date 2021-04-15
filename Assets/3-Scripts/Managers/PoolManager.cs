using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static Dictionary<string, PoolManager> cache;

    public static PoolManager GetPool(string id)
    {
        if (string.IsNullOrEmpty(id))
            return null;

        if (cache == null)
            cache = new Dictionary<string, PoolManager>();

        if (cache.ContainsKey(id))
        {
            return cache[id];
        }
        else
        {
            PoolManager[] pools = FindObjectsOfType<PoolManager>();
            for (int i = 0; i < pools.Length; i++)
            {
                if (pools[i].id.Equals(id))
                {
                    cache.Add(id, pools[i]);
                    return pools[i];
                }
            }
        }

        Debug.LogError($"Error: No pool found with the ID '{id}', check that the poll exists or create one.");
        return null;
    }

    private IPoolable[] poolEntities;
    private Queue<IPoolable> sleepingEntites;

    public string id;

    /// <summary>
    /// Clear an existing cache
    /// </summary>
    private void Awake()
    {
        cache?.Clear();
    }

    private void Start()
    {
        poolEntities = GetComponentsInChildren<IPoolable>();
        sleepingEntites = new Queue<IPoolable>();

        AttachSleepCallbackToAll((sender) =>
        {
            if (sleepingEntites.Contains(sender) == false)
                sleepingEntites.Enqueue(sender);
        });

        for (int i = 0; i < poolEntities.Length; i++)
            poolEntities[i].Sleep();
    }

    public void AttachAwakeCallbackToAll(PoolEvent callback)
    {
        for (int i = 0; i < poolEntities.Length; i++)
        {
            poolEntities[i].OnEntityAwoken += callback;
        }
    }

    public void AttachSleepCallbackToAll(PoolEvent callback)
    {
        for (int i = 0; i < poolEntities.Length; i++)
        {
            poolEntities[i].OnEntitySlept += callback;
        }
    }

    public IPoolable SpawnEntity()
    {
        if (sleepingEntites.Count == 0)
            return null;

        IPoolable entity = sleepingEntites.Dequeue();
        entity.Awaken();

        return entity;
    }

    public GameObject SpawnObject()
    {
        if (sleepingEntites.Count == 0)
            return null;

        IPoolable entity = sleepingEntites.Dequeue();
        entity.Awaken();

        return entity.gameObject;
    }

    public GameObject SpawnObject(Vector3 position)
    {
        if (sleepingEntites.Count == 0)
            return null;

        IPoolable entity = sleepingEntites.Dequeue();
        entity.Awaken();

        entity.gameObject.transform.position = position;

        return entity.gameObject;
    }

    public T SpawnAs<T>() where T : Component
    {
        if (sleepingEntites.Count == 0)
            return null;

        IPoolable entity = sleepingEntites.Dequeue();
        entity.Awaken();

        return entity.gameObject.GetComponent<T>();
    }

}