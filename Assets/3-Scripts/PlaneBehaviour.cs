using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneBehaviour : MonoBehaviour
{
    public delegate void PlaneEvent(PlaneBehaviour sender);
    public event PlaneEvent OnPlaneDestroyed;

    private bool awake = false;
    private float speed;
    private int maxHealth = 1;
    private int health = 1;

    private void Update()
    {
        if (awake)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;

            if (transform.position.x >= BoundsHelper.Instance.Right)
            {
                ResetHorizontal();
            }
        }
    }

    public void SetSpeed(float speed)
    {
        this.speed = Mathf.Clamp(speed, 0, float.MaxValue);
    }

    public void SetLevel(int level)
    {
        transform.position = new Vector3(transform.position.x, HeightHelper.Instance.GetHeightForLevel(level), transform.position.z);
    }

    public void ResetHorizontal()
    {
        transform.position = new Vector3(BoundsHelper.Instance.Left, transform.position.y, transform.position.z);
    }

    public void Damage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
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