using System;
using System.Collections;
using System.Collections.Generic;
using _0_Scripts.Events;
using _0_Scripts.GameManager;
using _0_Scripts.Level;
using _0_Scripts.UI;
using Enums;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UIPanelController uiPanelController;
    [SerializeField] private LevelManager levelManager;
    private bool isWin;

    private void Start()
    {
        Time.timeScale = 1;
        AdManager.instance.LoadInterstitialAd();
        AdManager.instance.LoadRewardedAd();
        
    }

    public void NextButton()
    {
        levelManager.LoadNextLevel();
        uiPanelController.ClosePanel(UIPanels.WinPanel);
        GameEvents.onLevelChange.Invoke();
        AdManager.instance.ShowAd();
        AudioManager.instance.StartThemeTrack();
    }

    public void RestartButton()
    {
        uiPanelController.ClosePanel(UIPanels.FailPanel);
        levelManager.ReloadLevel();
        GameEvents.onLevelChange.Invoke();
        AdManager.instance.ShowAd();
        AudioManager.instance.StartThemeTrack();
    }

    public void UpgradesButton()
    {
        GameManager.instance.camera.SetActive(false);
        uiPanelController.OpenPanel(UIPanels.UpgradePanel);
        uiPanelController.ClosePanel(UIPanels.InGamePanel);

        if (isWin)
        {
            uiPanelController.ClosePanel(UIPanels.WinPanel);
        }

        if (isWin == false)
        {
            uiPanelController.ClosePanel(UIPanels.FailPanel);
        }
    }

    public void ContinueButton()
    {
        GameManager.instance.camera.SetActive(true);
        uiPanelController.ClosePanel(UIPanels.UpgradePanel);
        uiPanelController.OpenPanel(UIPanels.InGamePanel);
        if (isWin)
        {
            uiPanelController.OpenPanel(UIPanels.WinPanel);
        }

        if (!isWin)
        {
            uiPanelController.OpenPanel(UIPanels.FailPanel);
        }
    }

    public void WinPanel()
    {
        isWin = true;
        uiPanelController.OpenPanel(UIPanels.WinPanel);
    }

    public void FailPanel()
    {
        isWin = false;
        uiPanelController.OpenPanel(UIPanels.FailPanel);
    }
}