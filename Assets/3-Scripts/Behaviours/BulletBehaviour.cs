using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour, IPoolable
{
    private bool awake = false;
    private Vector3 sleepPosition;

    private Vector2 direction;
    private float speed;
    private bool visiblity;

    private Vector2 lastUpdatePosition;

    public event PoolEvent OnEntityAwoken;
    public event PoolEvent OnEntitySlept;

    private void Awake()
    {
        sleepPosition = transform.position;
    }

    // Set the bullets flight parameters
    public void Initalise(Vector2 origin, Vector2 direction, float speed)
    {
        transform.position = origin;
        lastUpdatePosition = transform.position;

        this.direction = direction;
        this.speed = speed;
    }

    // Move the bullet each frame and check for and handle collisions
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

                    Sleep();
                    return;
                }
            }

            // Check if the bullet is still in the screen bounds and sleep it if not
            visiblity = BoundsHelper.Instance.IsPointInBounds(transform.position);

            if (visiblity == false)
            {
                Sleep();
            }

            lastUpdatePosition = transform.position;
        }
    }

    // Wake the bullet so that it can move
    public void Awaken()
    {
        awake = true;
        OnEntityAwoken?.Invoke(this);
    }

    // Sleep and high the bullet from view
    public void Sleep()
    {
        awake = false;
        transform.position = sleepPosition;
        OnEntitySlept?.Invoke(this);

    }

}
