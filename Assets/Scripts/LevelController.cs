using System;
using System.Collections;
using System.Collections.Generic;
using ChemicalMatch.Controllers;
using Final;
using UnityEngine;

public enum LevelType
{
    Freshman=1,
    Sophomore,
    Junior,
    Senior
}

public class LevelController : MonoSingleton<LevelController>
{
    public bool isFinish = false;
    public LevelType currentLevelType;

    private void OnEnable()
    {
        GameManager.Instance.IncreaseLevelNumber();
    }

    private void Awake()
    {
        GetCurrentLevel(GameManager.Instance.GetLevelNumber());
        
        FinishController.Instance.AddMethodFinishEvent(Finish);
    }

    private void GetCurrentLevel(int level)
    {
        currentLevelType = level switch
        {
            1 => LevelType.Freshman,
            2 => LevelType.Sophomore,
            3 => LevelType.Junior,
            4 => LevelType.Senior,
            _ => LevelType.Freshman
        };
    }

    private void Finish()
    {
        isFinish = true;
    }

}
