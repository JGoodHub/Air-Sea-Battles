using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    public enum AngleState
    {
        UP,
        MIDDLE,
        DOWN
    }

    [System.Serializable]
    public struct AimingSetup
    {
        public Sprite sprite;
        public float angle;
        [HideInInspector] public Vector2 angleDirection;
    }

    [Header("Team")]

    public Color gunColor;

    [Header("Positioning")]

    [Range(0.05f, 0.95f)] public float relativePosition;

    public float bulletOriginOffset;

    [Header("Sprite Setups")]

    public SpriteRenderer gunSpriteRen;

    [Space]

    public AimingSetup aimUpSetup;
    public AimingSetup aimMiddleSetup;
    public AimingSetup aimDownSetup;


    private Vector2 gunDirection;

    // Position the gun at the quarter mark
    private void Start()
    {
        Debug.Assert(aimUpSetup.sprite != null && aimMiddleSetup.sprite != null && aimDownSetup.sprite != null, "Not all angle sprites have been set");

        gunSpriteRen.color = gunColor;

        Vector2 cameraWorldSize = Camera.main.ViewportToWorldPoint(Vector2.one) - Camera.main.ViewportToWorldPoint(Vector2.zero);
        transform.position = new Vector3((-cameraWorldSize.x / 2) + (cameraWorldSize.x * relativePosition), 0f, 0f);

        aimUpSetup.angleDirection = RotateVector2(Vector2.right, aimUpSetup.angle, Vector2.zero).normalized;
        aimMiddleSetup.angleDirection = RotateVector2(Vector2.right, aimMiddleSetup.angle, Vector2.zero).normalized;
        aimDownSetup.angleDirection = RotateVector2(Vector2.right, aimDownSetup.angle, Vector2.zero).normalized;
    }

    // Poll for the aim and fire input events
    private void Update()
    {
        if (Input.GetButton("Aim Up"))
        {
            SetGunAngle(AngleState.UP);
        }
        else if (Input.GetButton("Aim Down"))
        {
            SetGunAngle(AngleState.DOWN);
        }
        else
        {
            SetGunAngle(AngleState.MIDDLE);
        }

        if (Input.GetButtonDown("Fire"))
        {
            FireBullet();
        }
    }

    public void SetGunAngle(AngleState state)
    {
        switch (state)
        {
            case AngleState.UP:
                gunSpriteRen.sprite = aimUpSetup.sprite;
                gunDirection = aimUpSetup.angleDirection;
                break;
            case AngleState.MIDDLE:
                gunSpriteRen.sprite = aimMiddleSetup.sprite;
                gunDirection = aimMiddleSetup.angleDirection;
                break;
            case AngleState.DOWN:
                gunSpriteRen.sprite = aimDownSetup.sprite;
                gunDirection = aimDownSetup.angleDirection;
                break;
            default:
                Debug.LogError("ERROR: AngleState state is of and invalid value");
                break;
        }
    }

    public bool FireBullet()
    {
        BulletManager.Instance.FireBullet((Vector2)transform.position + (gunDirection * bulletOriginOffset), gunDirection);

        return true;
    }

    /// <summary>
    /// Rotate a Vector2 X degrees around a pivot point
    /// </summary>
    /// <param name="point">The Vector2 to rotate</param>
    /// <param name="angle">The degrees to rotate (+ = CC, - = C)</param>
    /// <param name="pivot">The pivot used for the rotation</param>
    /// <returns>The rotated Vector2</returns>
    public static Vector2 RotateVector2(Vector2 point, float angle, Vector2 pivot)
    {
        float angleRadians = angle * Mathf.Deg2Rad;

        point -= pivot;

        float newX = (point.x * Mathf.Cos(angleRadians)) - (point.y * Mathf.Sin(angleRadians));
        float newY = (point.x * Mathf.Sin(angleRadians)) + (point.y * Mathf.Cos(angleRadians));

        return new Vector2(newX, newY) + pivot;
    }

}
