using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneBehaviour : MonoBehaviour
{

    public bool active = false;
    private float speed;
    private int health = 1;

    public BoundsHelper cameraBehaviour;

    private void Update()
    {
        if (active)
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
}
