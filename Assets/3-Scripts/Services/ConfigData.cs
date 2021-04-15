using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "Create Data Instance/Config")]
public class ConfigData : ScriptableObject
{
    private static ConfigData _instance;
    public static ConfigData Instance
    {
        get
        {
            if (_instance == null)
            {
                //TODO -----> Check for multiple copies of the singleton and notify user if found
                _instance = Resources.FindObjectsOfTypeAll<ConfigData>()[0];
                return _instance;
            }
            else
            {
                return _instance;
            }
        }
    }

    public string id;
    public int timeLimit;
    public int pointPerPlane;
    public int defaultHighScore;

    public int lastScore;
    public int highScore;

    public bool resetScoresOnInit;

    public void Initalise()
    {
        if (resetScoresOnInit)
        {
            lastScore = 0;
            highScore = 0;
        }
    }

    public int GetHighScore()
    {
        if (defaultHighScore > highScore)
            return defaultHighScore;
        else
            return highScore;
    }

    public bool SetHighScore(int newHighScore)
    {
        if (newHighScore > highScore)
        {
            highScore = newHighScore;
            return true;
        }
        else
        {
            return false;
        }
    }

    public override string ToString()
    {
        return $"ID: {id}\nTime Limit: {timeLimit}\nPoints Per Plane: {pointPerPlane}\nDefault Highscore: {defaultHighScore}";
    }

}