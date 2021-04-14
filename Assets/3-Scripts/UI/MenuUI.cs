using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{

    public Text lastScoreText;
    public Text highScoreText;

    private void Start()
    {
        lastScoreText.text = ConfigData.Instance.lastScore.ToString();
        highScoreText.text = ConfigData.Instance.highScore.ToString();

        ConfigService.Instance.OnConfigResponceRecieved -= OnConfigResponceRecieved;
        ConfigService.Instance.OnConfigResponceRecieved += OnConfigResponceRecieved;
    }

    private void OnConfigResponceRecieved(ConfigService sender, ConfigService.ConfigResponce responce)
    {
        highScoreText.text = ConfigData.Instance.GetHighScore().ToString();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire"))
        {
            LevelManager.Instance.LoadStageOne();
        }
    }

}
