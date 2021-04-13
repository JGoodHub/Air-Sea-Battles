using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : Singleton<PlaneManager>
{

    public PlaneBehaviour[] planePool;
    public int poolIndex;

    [Range(1, 8)] public int planesSpawnCountMin;
    [Range(1, 8)] public int planesSpawnCountMax;

    public float speed;
    public float waveDelay;

    private int planesAlive;

    private void OnValidate()
    {
        planesSpawnCountMax = Mathf.Clamp(planesSpawnCountMax, planesSpawnCountMin, 8);

        speed = Mathf.Clamp(speed, 0, float.MaxValue);
        waveDelay = Mathf.Clamp(waveDelay, 0, float.MaxValue);

        if (planePool != null && planePool.Length < planesSpawnCountMax)
        {
            Debug.LogWarning("Warning: Your plane pool is not big enough to support the max number of planes on screen at once, consider increasing your pool size");
        }
    }

    private void Start()
    {
        SpawnWave();
    }

    public void SpawnWave()
    {
        int planesCount = Random.Range(planesSpawnCountMin, planesSpawnCountMax);

        for (int p = 0; p < planesCount; p++)
        {
            planePool[poolIndex].active = true;
            planePool[poolIndex].SetSpeed(speed);
            planePool[poolIndex].SetLevel(8 - p);
            planePool[poolIndex].ResetHorizontal();

            poolIndex = (poolIndex + 1) % planePool.Length;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Vector3(BoundsHelper.Instance.Left, -25, 0), Vector3.up * 50);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Vector3(BoundsHelper.Instance.Right, -25, 0), Vector3.up * 50);

    }

}