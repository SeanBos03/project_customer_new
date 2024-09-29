using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainnequinHelp : MonoBehaviour
{
    //test script 
    bool infoAppear = true;
    [SerializeField] GameObject theInfo;
    GameManager gameManager;
    [SerializeField] int stageProgress = 1;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    public void StartInfo()
    {
        if (infoAppear)
        {
            theInfo.SetActive(true);
        }
    }

    public void DisableInfo()
    {
        theInfo.SetActive(false);
        infoAppear = false;
        gameManager.ProgressStage(stageProgress);
    }
}
