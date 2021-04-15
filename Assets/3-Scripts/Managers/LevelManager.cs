using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public int mainMenuIndex = 0;

    private int targetIndex;

    protected override void Awake()
    {
        base.Awake();
        if (dying)
            return;

        transform.parent = null;
        DontDestroyOnLoad(gameObject);
    }

    //Load the main menu scene with a slight delay
    public void LoadMainMenu(float delay)
    {
        LoadScene(mainMenuIndex, delay);
    }

    //Load the target scene index with a slight delay
    public void LoadScene(int buildIndex, float delay)
    {
        targetIndex = buildIndex;

        Invoke("LoadTargetScene", delay);
    }

    public void LoadTargetScene()
    {
        SceneManager.LoadScene(targetIndex);
    }

    // End a stage early by setting the time remaining to zero
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (SceneManager.GetActiveScene().buildIndex != mainMenuIndex)
                TimeManager.Instance.secondRemaining = 0;
        }
    }


}
