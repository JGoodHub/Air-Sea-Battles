using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetButtonDown("Fire"))
        {
            LevelManager.Instance.LoadStageOne();
        }
    }

}
