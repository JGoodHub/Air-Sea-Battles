using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : Singleton<PlaneManager>
{

    public delegate void WaveEvent();
    public event WaveEvent OnWaveStarted;
    public event WaveEvent OnWaveComplete;

    public PlaneBehaviour[] planePool;
    private Queue<PlaneBehaviour> sleepingPlanes;

    [Range(1, 8)] public int planesSpawnCountMin;
    [Range(1, 8)] public int planesSpawnCountMax;

    public float speed;
    public float waveDelay;

    private int planesAlive = -1;

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
        sleepingPlanes = new Queue<PlaneBehaviour>();
        for (int i = 0; i < planePool.Length; i++)
        {
            planePool[i].OnPlaneDestroyed += (plane) =>
            {
                planesAlive--;
                sleepingPlanes.Enqueue(plane);
            };

            sleepingPlanes.Enqueue(planePool[i]);
        }

        Invoke("SpawnWave", waveDelay);
    }

    private void Update()
    {
        if (planesAlive == 0)
        {
            OnWaveComplete?.Invoke();
            planesAlive = -1;

            Invoke("SpawnWave", waveDelay);
        }
    }

    public void SpawnWave()
    {
        int planesCount = Random.Range(planesSpawnCountMin, planesSpawnCountMax + 1);

        for (int p = 0; p < planesCount; p++)
        {
            PlaneBehaviour plane = sleepingPlanes.Dequeue();
            plane.Awaken();

            plane.SetSpeed(speed);
            plane.SetLevel(8 - p);
        }

        planesAlive = planesCount;

        OnWaveStarted?.Invoke();
    }

}