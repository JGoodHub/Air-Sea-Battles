using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{

    private int score;
    private int highscore = 100;

    private bool isNewHighscore = false;

    private int timeRemaining = 60;

    private void Start()
    {
        ScoreUI.Instance.SetScore(score);
        ScoreUI.Instance.SetHighscore(highscore);
        ScoreUI.Instance.SetTimeRemaining(timeRemaining);

        foreach (PlaneBehaviour planeBehaviour in PlaneManager.Instance.planePool)
        {
            planeBehaviour.OnPlaneDestroyed += (plane) =>
            {
                IncrementScore(5);
            };
        }

        TimeManager.Instance.OnTimerTick += (timer, secondsRemaining) =>
        {
            ScoreUI.Instance.SetTimeRemaining(secondsRemaining);
        };

        TimeManager.Instance.OnTimerExpired += (timer, secondsRemaining) =>
        {
            ScoreUI.Instance.DisplayGameoverScreen(isNewHighscore);
        };
    }

    public void IncrementScore(int amount)
    {
        Mathf.Clamp(amount, 0, int.MaxValue);

        score += amount;

        if (score > highscore)
        {
            highscore = score;
            isNewHighscore = true;
        }

        ScoreUI.Instance.SetScore(score);
        ScoreUI.Instance.SetHighscore(highscore);
    }

}
