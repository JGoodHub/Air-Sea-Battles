using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : Singleton<BulletManager>
{

    private Queue<BulletBehaviour> sleepingBullets;
    public BulletBehaviour[] bulletPool;

    public float projectileSpeed;

    private void Start()
    {
        sleepingBullets = new Queue<BulletBehaviour>();
        for (int i = 0; i < bulletPool.Length; i++)
        {
            bulletPool[i].OnBulletDestroyed += (bullet) =>
            {
                sleepingBullets.Enqueue(bullet);
            };

            sleepingBullets.Enqueue(bulletPool[i]);
        }
    }

    public bool FireBullet(Vector2 origin, Vector2 direction)
    {
        if (sleepingBullets.Count == 0)
            return false;

        BulletBehaviour bullet = sleepingBullets.Dequeue();

        bullet.Awaken();
        bullet.SetDirectionAndSpeed(direction, projectileSpeed);
        bullet.transform.position = origin;

        return true;
    }

}
