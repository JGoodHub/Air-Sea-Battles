using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : Singleton<ScoreUI>
{
    public Text scoreText;
    public Text timeText;
    public Text highscoreText;

    public GameObject gameOverScreen;
    public GameObject newHighscoreText;

    private void Start()
    {
        gameOverScreen.SetActive(false);
        newHighscoreText.SetActive(false);
    }

    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }

    //Convert the time remaining to minutes and seconds and display it
    public void SetTimeRemaining(int seconds)
    {
        seconds = Mathf.Clamp(seconds, 0, int.MaxValue);

        int minutes = Mathf.FloorToInt(seconds / 60f);
        seconds -= minutes * 60;

        timeText.text = $"{minutes}:{(seconds < 10 ? "0" : "")}{seconds}";
    }

    public void SetHighscore(int highscore)
    {
        highscoreText.text = highscore.ToString();
    }


    public void DisplayGameoverScreen(bool newHighscore)
    {
        gameOverScreen.SetActive(true);
        newHighscoreText.SetActive(newHighscore);
    }

}
