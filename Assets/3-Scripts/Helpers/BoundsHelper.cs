﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsHelper : Singleton<BoundsHelper>
{
    public Transform worldBoundsMin;
    public Transform worldBoundsMax;

    public float Left { get => worldBoundsMin.position.x; }
    public float Right { get => worldBoundsMax.position.x; }

    public float Top { get => worldBoundsMax.position.y; }
    public float Bottom { get => worldBoundsMin.position.y; }

    public bool IsPointInBounds(Vector2 point)
    {
        return point.x >= Left && point.x <= Right && point.y >= Bottom && point.y <= Top;
    }

    private void OnDrawGizmos()
    {
        if (worldBoundsMin == null)
            (worldBoundsMin = new GameObject("Bounds Min").transform).SetParent(transform);

        if (worldBoundsMax == null)
            (worldBoundsMax = new GameObject("Bounds Max").transform).SetParent(transform);

        Gizmos.color = Color.cyan;

        Gizmos.DrawWireCube((worldBoundsMin.position + worldBoundsMax.position) / 2, worldBoundsMax.position - worldBoundsMin.position);
    }

}
