﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : Singleton<TimeManager>
{
    public delegate void TimeEvent(TimeManager sender, int secondRemaining);
    public event TimeEvent OnTimerTick;
    public event TimeEvent OnTimerExpired;

    public int secondRemaining = 60;

    private void Start()
    {
        InvokeRepeating("DecrementTimeRemaining", 1f, 1f);
    }

    private void DecrementTimeRemaining()
    {
        secondRemaining -= 1;

        OnTimerTick?.Invoke(this, secondRemaining);

        if (secondRemaining == 0)
        {
            CancelInvoke();

            OnTimerExpired.Invoke(this, 0);

            Invoke("LoadMenuScene", 5f);
        }
    }

    private void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }
}