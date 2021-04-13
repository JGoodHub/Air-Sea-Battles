using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : Singleton<BulletManager>
{

    public BulletBehaviour[] bulletPool;
    public int poolIndex;

    public int maxProjectilesOnScreen;
    private int projectilesOnScreen;

    public float projectileSpeed;

    private void OnValidate()
    {
        maxProjectilesOnScreen = Mathf.Clamp(maxProjectilesOnScreen, 0, int.MaxValue);

        if (bulletPool != null && bulletPool.Length < maxProjectilesOnScreen)
        {
            Debug.LogWarning("Warning: Your bullet pool is not big enough to support the max number of bullets on screen at once, consider increasing your pool size");
        }
    }

    private void Start()
    {
        for (int b = 0; b < bulletPool.Length; b++)
        {
            bulletPool[b].OnBulletOutsideScreen += ResetBullet;
        }
    }

    public bool FireBullet(Vector2 origin, Vector2 direction)
    {
        poolIndex = (poolIndex + 1) % bulletPool.Length;

        bulletPool[poolIndex].SetDirectionAndSpeed(direction, projectileSpeed);
        bulletPool[poolIndex].transform.position = origin;
        bulletPool[poolIndex].active = true;

        return true;
    }

    private void ResetBullet(BulletBehaviour bullet)
    {
        bullet.active = false;
        bullet.transform.position = Vector3.down * 10f;
    }

}
