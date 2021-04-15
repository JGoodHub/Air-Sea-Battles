using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : Singleton<PlaneManager>
{

    public delegate void WaveEvent();
    public event WaveEvent OnWaveStarted;
    public event WaveEvent OnWaveComplete;

    [Range(1, 8)] public int planesSpawnCountMin;
    [Range(1, 8)] public int planesSpawnCountMax;

    public float waveDelay;

    private int planesAlive = 0;

    private void OnValidate()
    {
        planesSpawnCountMax = Mathf.Clamp(planesSpawnCountMax, planesSpawnCountMin, 8);
        waveDelay = Mathf.Clamp(waveDelay, 0, float.MaxValue);
    }

    // Setup the callbacks to check when all the planes have been destroyed and the next wave can be spawned
    private void Start()
    {
        PoolManager.GetPool("Aircraft").AttachSleepCallbackToAll((sender) =>
        {
            planesAlive--;

            if (planesAlive == 0)
            {
                OnWaveComplete?.Invoke();
                Invoke("SpawnWave", waveDelay);
            }
        });

        Invoke("SpawnWave", waveDelay);
    }

    //Spawn a random number of planes and start them flying across the screen
    public void SpawnWave()
    {
        int planesCount = Random.Range(planesSpawnCountMin, planesSpawnCountMax + 1);

        for (int p = 0; p < planesCount; p++)
        {
            PlaneBehaviour plane = PoolManager.GetPool("Aircraft").SpawnAs<PlaneBehaviour>();
            plane.SetHeightLevel(8 - p);
        }

        planesAlive = planesCount;

        OnWaveStarted?.Invoke();
    }

}