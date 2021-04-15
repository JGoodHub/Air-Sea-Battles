using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativePositioner : MonoBehaviour
{

    public new Camera camera;

    [Range(-1f, 2f)] public float relativePositionX;
    [Range(-1f, 2f)] public float relativePositionY;

    //Set the transform to the relative position
    private void Awake()
    {
        Vector2 relativeWorldPosition = RelativePointToWorldPosition(relativePositionX, relativePositionY);
        transform.position = new Vector3(relativeWorldPosition.x, relativeWorldPosition.y, transform.position.z);
    }

    //Convert a relative screen coordinate to a world space coordinate, useful for positioning items on dynamic aspect ratios
    public Vector2 RelativePointToWorldPosition(float relativePositionX, float relativePositionY)
    {
        if (camera == null)
            camera = Camera.main;

        Vector2 cameraWorldSize = camera.ViewportToWorldPoint(Vector2.one) - camera.ViewportToWorldPoint(Vector2.zero);
        return new Vector2(
            camera.transform.position.x + (-cameraWorldSize.x / 2) + (cameraWorldSize.x * relativePositionX),
            camera.transform.position.y + (-cameraWorldSize.y / 2) + (cameraWorldSize.y * relativePositionY));
    }

    public bool updateInEditor;

    // Update the position in the editor
    private void OnDrawGizmos()
    {
        if (updateInEditor)
        {
            Vector2 relativeWorldPosition = RelativePointToWorldPosition(relativePositionX, relativePositionY);
            transform.position = new Vector3(relativeWorldPosition.x, relativeWorldPosition.y, transform.position.z);
        }
    }

}