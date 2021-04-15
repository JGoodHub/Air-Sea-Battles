using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExplosionBehaviour : MonoBehaviour, IPoolable
{
    private Vector3 sleepPosition;

    public float effectDuration;
    private float sleepCountdown;

    public UnityEvent OnExplosionAwoken;
    public UnityEvent OnExplosionSlept;

    public event PoolEvent OnEntityAwoken;
    public event PoolEvent OnEntitySlept;

    private void Awake()
    {
        sleepPosition = transform.position;
    }

    private void Update()
    {
        if (sleepCountdown > 0)
        {
            sleepCountdown -= Time.deltaTime;
            if (sleepCountdown <= 0)
            {
                Sleep();
            }
        }
    }

    public void Awaken()
    {
        OnEntityAwoken?.Invoke(this);
        OnExplosionAwoken?.Invoke();

        sleepCountdown = effectDuration;
    }

    public void Sleep()
    {
        OnEntitySlept?.Invoke(this);
        OnExplosionSlept?.Invoke();

        transform.position = sleepPosition;
    }
}
