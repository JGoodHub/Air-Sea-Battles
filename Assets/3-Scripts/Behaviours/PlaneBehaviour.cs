using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneBehaviour : MonoBehaviour
{
    public delegate void PlaneEvent(PlaneBehaviour sender);
    public event PlaneEvent OnPlaneDestroyed;

    private bool awake = false;

    private int maxHealth = 1;
    private int health = 1;

    private float t;
    private Vector3 linearPosition;

    public float averageSpeed;
    private float traverselDistance;
    private float traversalTime;
    public AnimationCurve traverselCurve;

    private void OnValidate()
    {
        maxHealth = Mathf.Clamp(maxHealth, 1, int.MaxValue);
        averageSpeed = Mathf.Clamp(averageSpeed, 0, float.MaxValue);
    }

    private void Update()
    {
        if (awake)
        {
            t += Time.deltaTime;

            linearPosition.x = BoundsHelper.Instance.Left + (traverselCurve.Evaluate(t / traversalTime) * traverselDistance);

            transform.position = linearPosition;

            if (t >= traversalTime)
            {
                ResetHorizontal();
            }
        }
    }

    public void SetHeightLevel(int level)
    {
        linearPosition.y = HeightHelper.Instance.GetHeightForLevel(level);
    }

    public void ResetHorizontal()
    {
        linearPosition = new Vector3(BoundsHelper.Instance.Left, transform.position.y, transform.position.z);
        t = 0;

        traverselDistance = BoundsHelper.Instance.Right - BoundsHelper.Instance.Left;
        traversalTime = traverselDistance / averageSpeed;
    }

    public void Damage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            ExplosionsManager.Instance.SpawnExplosion(transform.position);

            Sleep();

            OnPlaneDestroyed?.Invoke(this);
        }
    }

    public void Awaken()
    {
        awake = true;
        health = maxHealth;
        ResetHorizontal();
    }

    public void Sleep()
    {
        awake = false;
        transform.position = Vector3.down * 10f;
    }

}