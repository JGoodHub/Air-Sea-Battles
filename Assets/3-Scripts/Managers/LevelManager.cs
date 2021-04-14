using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public int mainMenuIndex = 0;
    public int stageOneIndex = 1;

    public void LoadMainMenu(float delay)
    {
        Invoke("LoadMainMenu", delay);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuIndex);
    }

    public void LoadStageOne(float delay)
    {
        Invoke("LoadStageOne", delay);
    }

    public void LoadStageOne()
    {
        SceneManager.LoadScene(stageOneIndex);
    }


}
