using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _0_Scripts.Level
{
    public class LevelManager : MonoBehaviour
    {
        public TextMeshProUGUI levelText;
        [SerializeField] private List<GameObject> levelPrefabs;

        private int _currentLevel = 0;
        public GameObject _activeLevelPrefab;

        public int _levelCounter = 1;

        private void Awake()
        {
            GameAnalyticsSDK.GameAnalytics.Initialize();
            if (PlayerPrefs.HasKey("CurrentLevel"))
            {
                _currentLevel = PlayerPrefs.GetInt("CurrentLevel");
            }
            else
            {
                _currentLevel = 0;
                PlayerPrefs.SetInt("CurrentLevel", _currentLevel);
            }

            if (PlayerPrefs.HasKey("LevelTextCounter"))
            {
                _levelCounter = PlayerPrefs.GetInt("LevelTextCounter");
            }
            else
            {
                _levelCounter = 1;
                PlayerPrefs.SetInt("LevelTextCounter", _levelCounter);
            }

            UpdateLevelText();
        }

        private void OnApplicationQuit()
        {
            PlayerPrefs.SetInt("CurrentLevel", _currentLevel);
            PlayerPrefs.SetString("LevelText", levelText.text);
            PlayerPrefs.SetInt("LevelTextCounter", _levelCounter);

            PlayerPrefs.Save();
        }

        public void LoadNextLevel()
        {
            _currentLevel++;
            if (_currentLevel >= levelPrefabs.Count)
            {
                _currentLevel = 0;
            }

            _levelCounter++;
            ClearActiveLevel();
            LoadLevel(_currentLevel);
            UpdateLevelText();
        }

        public void ReloadLevel()
        {
            ClearActiveLevel();
            LoadLevel(_currentLevel);
            SceneManager.LoadScene("AliLastScene");
            
        }

        private void LoadLevel(int levelIndex)
        {
            if (levelIndex < 0 || levelIndex >= levelPrefabs.Count)
            {
                return;
            }

            ClearActiveLevel();

            _activeLevelPrefab = Instantiate(levelPrefabs[levelIndex]).gameObject;
            QuestManager.instance.SetQuest(_activeLevelPrefab.GetComponent<LevelInfo>());
        }

        private void ClearActiveLevel()
        {
            if (_activeLevelPrefab != null)
            {
                Destroy(_activeLevelPrefab);
            }
        }

        private void UpdateLevelText()
        {
            levelText.text = "Level: " + _levelCounter;
            GameAnalyticsEvents.instance.LevelStarted(_levelCounter);
        }
    }
}