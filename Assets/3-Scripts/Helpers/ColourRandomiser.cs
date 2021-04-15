using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourRandomiser : MonoBehaviour
{

    public new SpriteRenderer renderer;

    public Color[] colors;

    private void Start()
    {
        if (colors != null && colors.Length > 0)
        {
            renderer.color = colors[Random.Range(0, colors.Length)];
        }
    }

}
