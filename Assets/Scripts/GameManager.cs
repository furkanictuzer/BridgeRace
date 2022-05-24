using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using ChemicalMatch.Controllers;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public int levelNumber = 0;

    public LevelType CurrentLevelType => GetLevelType();

    private void OnEnable()
    {
        DontDestroyOnLoad(this);
    }

    private LevelType GetLevelType()
    {
        var level = levelNumber switch
        {
            1 => LevelType.Freshman,
            2 => LevelType.Sophomore,
            3 => LevelType.Junior,
            4 => LevelType.Senior,
            _ => LevelType.Freshman
        };

        return level;
    }

    public int GetLevelNumber()
    {
        return levelNumber;
    }

    public void IncreaseLevelNumber()
    {
        levelNumber++;
    }
}
