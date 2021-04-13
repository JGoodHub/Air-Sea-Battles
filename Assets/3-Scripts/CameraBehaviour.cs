using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private Vector2 worldBoundsMin;
    private Vector2 worldBoundsMax;

    public Camera camera;

    private void Start()
    {
        worldBoundsMin = Camera.main.ViewportToWorldPoint(Vector2.zero);
        worldBoundsMax = Camera.main.ViewportToWorldPoint(Vector2.one);
    }

    public bool IsPointInCameraBounds(Vector2 point)
    {
        return point.x > worldBoundsMin.x && point.x < worldBoundsMax.x && point.y > worldBoundsMin.y && point.y < worldBoundsMax.y;
    }

}
