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

    public void LoadMainMenu(float delay)
    {
        Invoke("LoadMainMenu", delay);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuIndex);
    }

    public void LoadScene(int buildIndex, float delay)
    {
        targetIndex = buildIndex;

        Invoke("LoadTargetScene", delay);
    }

    public void LoadTargetScene()
    {
        SceneManager.LoadScene(targetIndex);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            TimeManager.Instance.secondRemaining = 0;
        }
    }


}
