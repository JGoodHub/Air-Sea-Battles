using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightHelper : Singleton<HeightHelper>
{

    public float zeroHeight;
    public float spacing;

    public int levels;


    public float GetHeightForLevel(int level)
    {
        level = Mathf.Clamp(level, 0, levels);

        return zeroHeight + (spacing * level);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        for (int l = 0; l < levels; l++)
        {
            Gizmos.DrawRay(new Vector3(0, GetHeightForLevel(l), 0) + (Vector3.left * 15), Vector3.right * 30);
        }
    }



}
