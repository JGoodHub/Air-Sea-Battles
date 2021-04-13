using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsHelper : Singleton<BoundsHelper>
{
    public Vector2 WorldBoundsMin { get; private set; }
    public Vector2 WorldBoundsMax { get; private set; }

    public Vector2 offset;
    public Vector2 size;

    public float Left { get => offset.x - (size.x / 2); }
    public float Right { get => offset.x + (size.x / 2); }

    public float Top { get => offset.y + (size.y / 2); }
    public float Bottom { get => offset.y - (size.y / 2); }

    private void Start()
    {
        WorldBoundsMin = Camera.main.ViewportToWorldPoint(Vector2.zero);
        WorldBoundsMax = Camera.main.ViewportToWorldPoint(Vector2.one);
    }

    public bool IsPointInBounds(Vector2 point)
    {
        return point.x >= Left && point.x <= Right && point.y >= Bottom && point.y <= Top;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(offset, size);
    }

}
