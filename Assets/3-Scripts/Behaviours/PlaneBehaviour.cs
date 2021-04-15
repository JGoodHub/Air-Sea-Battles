using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneBehaviour : MonoBehaviour, IPoolable
{
    public event PoolEvent OnEntityAwoken;
    public event PoolEvent OnEntitySlept;

    private bool awake = false;
    private Vector2 sleepPosition;

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

    private void Awake()
    {
        sleepPosition = transform.position;
        linearPosition.z = transform.position.z;
    }

    //Set the parameters needed to traverse using the curve
    private void Start()
    {
        traverselDistance = BoundsHelper.Instance.Right - BoundsHelper.Instance.Left;
        traversalTime = traverselDistance / averageSpeed;
    }

    //Fly across the screen using the curve as reference for how fast at each moment as we cross the screen, resetting to the start when we reach the other side
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

    //Set which height/colour bound this plane is in using the helper
    public void SetHeightLevel(int level)
    {
        linearPosition.y = HeightHelper.Instance.GetHeightForLevel(level);
    }

    //Reset our horizontal position to the left side of the screen
    public void ResetHorizontal()
    {
        linearPosition = new Vector3(BoundsHelper.Instance.Left, transform.position.y, transform.position.z);
        t = 0;
    }

    //Deal the passed amount of damage to our plane and if were out of hp, sleep and create an explosion in our place
    public void Damage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            PoolManager.GetPool("Explosions").SpawnObject(transform.position);

            Sleep();
        }
    }

    #region Interface Methods

    public void Awaken()
    {
        awake = true;
        health = maxHealth;
        ResetHorizontal();

        OnEntityAwoken?.Invoke(this);
    }

    public void Sleep()
    {
        awake = false;
        transform.position = sleepPosition;
        OnEntitySlept?.Invoke(this);
    }

    #endregion

}