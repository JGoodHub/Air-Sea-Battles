using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{

    private int localScore;
    private int localHighScore;

    private bool isNewHighscore = false;

    private void Start()
    {
        localHighScore = ConfigData.Instance.GetHighScore();

        ScoreUI.Instance.SetScore(localScore);
        ScoreUI.Instance.SetHighscore(localHighScore);
        ScoreUI.Instance.SetTimeRemaining(ConfigData.Instance.timeLimit);

        PoolManager.GetPool("Aircraft").AttachSleepCallbackToAll((plane) =>
        {
            IncrementScore(ConfigData.Instance.pointPerPlane);
        });

        TimeManager.Instance.OnTimerTick += (timer, secondsRemaining) =>
        {
            ScoreUI.Instance.SetTimeRemaining(secondsRemaining);
        };

        TimeManager.Instance.OnTimerExpired += (timer, secondsRemaining) =>
        {
            ScoreUI.Instance.DisplayGameoverScreen(isNewHighscore);

            ConfigData.Instance.lastScore = localScore;
            ConfigData.Instance.SetHighScore(localHighScore);
        };
    }

    public void IncrementScore(int amount)
    {
        if (TimeManager.Instance.Expired == false)
        {
            Mathf.Clamp(amount, 0, int.MaxValue);

            localScore += amount;

            if (localScore > localHighScore)
            {
                localHighScore = localScore;
                isNewHighscore = true;
            }

            ScoreUI.Instance.SetScore(localScore);
            ScoreUI.Instance.SetHighscore(localHighScore);
        }
    }

}
