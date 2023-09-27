using System;
using System.Collections.Generic;
using _0_Scripts.Collectables;
using _0_Scripts.Events;
using _0_Scripts.Level;
using Unity.VisualScripting;
using UnityEngine;

namespace _0_Scripts.GameManager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public QuestManager questManager;
        public UIManager uiManager;
        public GameObject camera;

        [SerializeField] private UpgradeManager upgradeManager;
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private int currentLevel;
        [SerializeField] private List<GameObject> levelPrefabs;
        [SerializeField] private GameObject ufo;


        private Vector3 ufoStartPosition;
        private bool _isRewardAdded = false;


        private void Awake()
        {
            GameAnalyticsSDK.GameAnalytics.Initialize();
            ;
            instance = this;
            levelManager._activeLevelPrefab = Instantiate(levelPrefabs[currentLevel]);
            questManager.SetQuest(levelManager._activeLevelPrefab.GetComponent<LevelInfo>());
            ufoStartPosition = new Vector3(0, 11.5f, 0);
            ufo.transform.position = ufoStartPosition;
        }


        private void Start()
        {
            GameAnalyticsEvents.instance.LevelStarted(currentLevel);
            AudioManager.instance.StartThemeTrack();
        }


        private void OnEnable()
        {
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            GameEvents.onWin.AddListener(OnWin);
            GameEvents.onFail.AddListener(OnFail);
            GameEvents.onCoinAdded.AddListener(OnCoinAdded);
            GameEvents.onLevelChange.AddListener(OnLevelChange);
        }

        private void UnRegisterEvents()
        {
            GameEvents.onWin.RemoveListener(OnWin);
            GameEvents.onFail.RemoveListener(OnFail);
            GameEvents.onCoinAdded.RemoveListener(OnCoinAdded);
            GameEvents.onLevelChange.RemoveListener(OnLevelChange);
        }


        private void OnDisable()
        {
            UnRegisterEvents();
        }

        private void OnWin()
        {
            uiManager.WinPanel();
            if (!_isRewardAdded)
            {
                upgradeManager.AddRewardMoney(200);
                _isRewardAdded = true;
            }

            AudioManager.instance.StopThemeTrack();
            GameAnalyticsEvents.instance.LevelCompleted(levelManager._levelCounter);
        }

        private void OnFail()
        {
            uiManager.FailPanel();
            AudioManager.instance.StopThemeTrack();
            GameAnalyticsEvents.instance.LevelFailed(levelManager._levelCounter);
        }

        private void OnLevelChange()
        {
            ufoStartPosition = new Vector3(0, 11.5f, 0);
            ufo.transform.position = ufoStartPosition;
            _isRewardAdded = false;
        }

        private void OnCoinAdded()
        {
            upgradeManager.AddCoinMoney(50);
        }
    }
}