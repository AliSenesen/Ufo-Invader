using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Odbc;
using GameAnalyticsSDK;
using UnityEngine;

public class GameAnalyticsEvents : MonoBehaviour
{
    public static GameAnalyticsEvents instance;

    private void Awake()
    {
        instance = this;
    }

    public void LevelStarted(int Level)
    {
        GameAnalytics.NewDesignEvent("LevelStarted_" + Level);
    }

    public void LevelCompleted(int Level)
    {
        GameAnalytics.NewDesignEvent("LevelCompleted_" + Level);
    }

    public void LevelFailed(int Level)
    {
        GameAnalytics.NewDesignEvent("LevelFailed" + Level);
    }
}