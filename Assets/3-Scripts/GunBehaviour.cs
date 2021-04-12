using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    [System.Serializable]
    public struct AimingSetup
    {
        public string name;
        public Sprite sprite;
        public float angle;
    }


    [Header("Positioning")]

    [Range(0.05f, 0.95f)] public float relativePosition;

    [Header("Gun Sprites")]

    public AimingSetup aimUpSetup;
    public AimingSetup aimMiddleSetup;
    public AimingSetup aimDownSetup;

    public SpriteRenderer gunSpriteRen;

    // Position the gun at the quarter mark
    private void Start()
    {
        Debug.Assert(aimUpSetup.sprite != null && aimMiddleSetup.sprite != null && aimDownSetup.sprite != null, "Not all angle sprites have been set");

        Vector2 cameraWorldSize = Camera.main.ViewportToWorldPoint(Vector2.one) - Camera.main.ViewportToWorldPoint(Vector2.zero);
        transform.position = new Vector3((-cameraWorldSize.x / 2) + (cameraWorldSize.x * relativePosition), 0f, 0f);
    }

    private void Update()
    {
        if (Input.GetButton("Aim Up"))
        {
            gunSpriteRen.sprite = aimUpSetup.sprite;
        }
        else if (Input.GetButton("Aim Down"))
        {
            gunSpriteRen.sprite = aimDownSetup.sprite;
        }
        else
        {
            gunSpriteRen.sprite = aimMiddleSetup.sprite;
        }

        if (Input.GetButtonDown("Fire"))
        {

        }
    }

    public static Vector2 RotateVector2(float angle, Vector2 pivot)
    {
        return Vector2.zero;
    }

}
