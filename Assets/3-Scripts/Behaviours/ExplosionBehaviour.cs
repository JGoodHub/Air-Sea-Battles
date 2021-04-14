using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExplosionBehaviour : MonoBehaviour
{
    public float effectDuration;
    private float sleepCountdown;

    public UnityEvent onExplosionAwoken;
    public UnityEvent onExplosionSlept;

    private void Start()
    {
        Sleep();
    }

    public void Awaken()
    {
        onExplosionAwoken?.Invoke();

        sleepCountdown = effectDuration;
    }

    public void Sleep()
    {
        onExplosionSlept?.Invoke();

        transform.position = Vector2.down * 6f;
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


}
