using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRandomiser : MonoBehaviour
{
    public float randomiseInterval = 5f;

    public new SpriteRenderer renderer;

    public Sprite[] sprites;

    // Randomly cycle through the sprite array
    private void Start()
    {
        InvokeRepeating("RandomiseSprite", randomiseInterval, randomiseInterval);
    }

    private void RandomiseSprite()
    {
        renderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }
}
