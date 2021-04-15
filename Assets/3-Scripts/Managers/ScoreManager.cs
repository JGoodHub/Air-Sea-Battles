using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{

    private int localScore;
    private int localHighScore;

    private bool isNewHighscore = false;

    //Set the score texts using the global config file and the event callbacks for show game overs screens when the timer has expired
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

        //Update the time remaining text
        TimeManager.Instance.OnTimerTick += (timer, secondsRemaining) =>
        {
            ScoreUI.Instance.SetTimeRemaining(secondsRemaining);
        };

        //Show the game over screen and potentially highscore text when the timer has finished
        TimeManager.Instance.OnTimerExpired += (timer, secondsRemaining) =>
        {
            ScoreUI.Instance.DisplayGameoverScreen(isNewHighscore);

            ConfigData.Instance.lastScore = localScore;
            ConfigData.Instance.SetHighScore(localHighScore);
        };
    }

    //If the timer hasn't expired increase the players score by the set amount and update the UI to match
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
