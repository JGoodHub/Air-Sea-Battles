using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionsManager : Singleton<ExplosionsManager>
{

    private Queue<ExplosionBehaviour> sleepingExplosions;
    public ExplosionBehaviour[] explosionPool;

    private void Start()
    {
        sleepingExplosions = new Queue<ExplosionBehaviour>();
        for (int i = 0; i < explosionPool.Length; i++)
        {
            int iRef = i;

            explosionPool[i].onExplosionSlept.AddListener(() =>
            {
                sleepingExplosions.Enqueue(explosionPool[iRef]);
            });
        }
    }

    public bool SpawnExplosion(Vector2 position)
    {
        if (sleepingExplosions.Count == 0)
            return false;

        ExplosionBehaviour explosion = sleepingExplosions.Dequeue();

        explosion.transform.position = position;
        explosion.Awaken();

        return true;
    }


}
