using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : Singleton<TimeManager>
{
    public delegate void TimeEvent(TimeManager sender, int secondRemaining);
    public event TimeEvent OnTimerTick;
    public event TimeEvent OnTimerExpired;

    public int secondRemaining;

    public bool Expired { get => secondRemaining <= 0; }

    //Set the time limit from the config
    private void Start()
    {
        secondRemaining = ConfigData.Instance.timeLimit;

        InvokeRepeating("DecrementTimeRemaining", 1f, 1f);
    }

    //Decrease the seconds remaining by 1 and check if the timer has expired, calling the appropriate events if it has
    private void DecrementTimeRemaining()
    {
        secondRemaining -= 1;

        OnTimerTick?.Invoke(this, secondRemaining);

        if (secondRemaining <= 0)
        {
            CancelInvoke();

            OnTimerExpired.Invoke(this, 0);

            LevelManager.Instance.LoadMainMenu(4f);
        }
    }
}
