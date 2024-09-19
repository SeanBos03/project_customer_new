using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    int theStage = 0;
    [SerializeField] int levelCompleteStage = 3;
    [SerializeField] TMP_Text stageText;
    bool levelComplete = false;

    void Start()
    {
        stageText.text = "People helped: " + theStage + "/" + levelCompleteStage;
    }

    public void ProgressStage(int theAmount)
    {
        theStage += theAmount;
        stageText.text = "People helped: " + theStage + "/" + levelCompleteStage;
        CheckLevelComplete();
    }

    void CheckLevelComplete()
    {
        if (!levelComplete)
        {
            if (theStage == levelCompleteStage)
            {
                levelComplete = true;
                Debug.Log("Level complete");
            }
        }
    }
}
