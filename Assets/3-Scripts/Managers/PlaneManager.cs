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