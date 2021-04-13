using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{

    public bool active = false;

    private Vector2 direction;
    private float speed;
    private bool visiblity;

    private Vector2 lastUpdatePosition;
    private bool lastUpdateVisibility;

    private CameraBehaviour cameraBehaviour;

    public delegate void BulletOutsideScreen(BulletBehaviour sender);
    public event BulletOutsideScreen OnBulletOutsideScreen;

    private void Start()
    {
        cameraBehaviour = FindObjectOfType<CameraBehaviour>();
    }

    public void SetDirectionAndSpeed(Vector2 direction, float speed)
    {
        this.direction = direction;
        this.speed = speed;
    }

    private void FixedUpdate()
    {
        if (active)
        {
            transform.position += (Vector3)(direction * speed * Time.fixedDeltaTime);

            // Do a raycast from our last position to now to see if we hit anything along the way
            RaycastHit2D rayHit;
            if (Physics2D.Raycast(lastUpdatePosition, (Vector2)transform.position - lastUpdatePosition, speed * Time.fixedDeltaTime))
            {

            }

            visiblity = cameraBehaviour.IsPointInCameraBounds(transform.position);

            if (visiblity == false && lastUpdateVisibility == true)
            {
                OnBulletOutsideScreen?.Invoke(this);
            }

            lastUpdatePosition = transform.position;
            lastUpdateVisibility = visiblity;
        }
    }



}
