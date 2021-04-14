using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "Create Data Instance/Config")]
public class ConfigData : ScriptableObject
{
    public static ConfigData Instance;

    public string id;
    public int timeLimit;
    public int pointPerPlane;
    public int defaultHighScore;

    public int lastScore;
    public int highScore;

    public bool resetScoresOnInit;

    public void Initalise()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

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