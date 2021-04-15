using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectorUI : MonoBehaviour
{

    [System.Serializable]
    public struct StageSetup
    {
        public string name;
        public int buildIndex;
    }

    [Header("Stages")]

    public StageSetup[] stages;
    private int stageIndex = 0;

    [Header("UI Elements")]

    public Text stageText;

    public GameObject stageLeftArrow;
    public GameObject stageRightArrow;

    private void Start()
    {
        ShowStage(stageIndex);
    }

    private void Update()
    {
        Debug.Log(Input.GetButtonDown("Aim Left"));

        if (Input.GetButtonDown("Aim Left"))
        {
            stageIndex = Mathf.Clamp(stageIndex - 1, 0, stages.Length - 1);
            ShowStage(stageIndex);
        }

        if (Input.GetButtonDown("Aim Right"))
        {
            stageIndex = Mathf.Clamp(stageIndex + 1, 0, stages.Length - 1);
            ShowStage(stageIndex);
        }

        if (Input.GetButtonDown("Fire"))
        {
            LevelManager.Instance.LoadScene(stages[stageIndex].buildIndex, 0f);
        }
    }

    public void ShowStage(int index)
    {
        index = Mathf.Clamp(index, 0, stages.Length - 1);

        stageText.text = stages[index].name;

        stageLeftArrow.SetActive(index > 0);
        stageRightArrow.SetActive(index < stages.Length - 1);
    }
}
