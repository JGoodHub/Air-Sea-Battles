using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public delegate void BulletEvent(BulletBehaviour sender);
    public event BulletEvent OnBulletDestroyed;

    private bool awake = false;

    private Vector2 direction;
    private float speed;
    private bool visiblity;

    private Vector2 lastUpdatePosition;

    public void SetDirectionAndSpeed(Vector2 direction, float speed)
    {
        this.direction = direction;
        this.speed = speed;
    }

    private void Update()
    {
        if (awake)
        {
            transform.position += (Vector3)(direction * speed * Time.deltaTime);

            // Do a raycast from our last position to now to see if we hit anything along the way
            RaycastHit2D rayHit = Physics2D.Raycast(lastUpdatePosition, (Vector2)transform.position - lastUpdatePosition, Vector2.Distance(lastUpdatePosition, transform.position));

            if (rayHit.collider != null)
            {
                PlaneBehaviour plane = rayHit.collider.GetComponent<PlaneBehaviour>();

                if (plane != null)
                {
                    plane.Damage(1);

                    OnBulletDestroyed?.Invoke(this);
                    Sleep();
                }
            }

            visiblity = BoundsHelper.Instance.IsPointInBounds(transform.position);

            if (visiblity == false)
            {
                OnBulletDestroyed?.Invoke(this);
                Sleep();
            }

            lastUpdatePosition = transform.position;
        }
    }

    public void Awaken()
    {
        awake = true;
    }

    public void Sleep()
    {
        awake = false;
        transform.position = Vector3.down * 5f;
    }

}
