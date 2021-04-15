using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : Singleton<BulletManager>
{

    public float projectileSpeed;

    public bool FireBullet(Vector2 origin, Vector2 direction)
    {
        BulletBehaviour bullet = PoolManager.GetPool("Bullets").SpawnAs<BulletBehaviour>();

        if (bullet != null)
        {
            bullet.Initalise(origin, direction, projectileSpeed);
            return true;
        }
        else
        {
            return false;
        }
    }

}
