using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "Create Data Instance/Config")]
public class ConfigData : ScriptableObject
{
    public string id;

    public int pointPerPlane;
    public int defaultHighScore;
    public int timeLimit;
}
