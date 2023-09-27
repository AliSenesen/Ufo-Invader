using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using _0_Scripts.Enums;
using _0_Scripts.Events;
using _0_Scripts.Level;
using DG.Tweening;

namespace _0_Scripts.UI
{
    public class QuestItemUI : MonoBehaviour
    {
        public Image QuestImage, CheckImage, BackgroundImage;
        public QuestItems questItems;
        public Sprite UnCompleteBackground;
        public Sprite CompleteBackground;

        [SerializeField] private TextMeshProUGUI countTxt;


        private void OnEnable()
        {
            GameEvents.onWin.AddListener(OnLevelChange);
            GameEvents.onFail.AddListener(OnLevelChange);
        }

        private void OnDisable()
        {
            GameEvents.onWin.RemoveListener(OnLevelChange);
            GameEvents.onFail.RemoveListener(OnLevelChange);
        }


        public void SetQuestText(QuestItems _questItems, Sprite mySprite)
        {
            questItems = _questItems;
            QuestImage.sprite = mySprite;
            countTxt.text = questItems.targetCount.ToString();
        }

        public void DecreaseItemCount()
        {
            questItems.targetCount--;
            if (questItems.targetCount == 0)
            {
                GameManager.GameManager.instance.questManager.questItemUIList.Remove(this);
                if (GameManager.GameManager.instance.questManager.questItemUIList.Count == 0)
                {
                    GameEvents.onWin.Invoke();
                }

                countTxt.gameObject.SetActive(false);
                CheckImage.gameObject.SetActive(true);
                BackgroundImage.sprite = CompleteBackground;
            }
            else
            {
                countTxt.text = questItems.targetCount.ToString();
                BackgroundImage.sprite = UnCompleteBackground;
            }
        }


        private void OnLevelChange()
        {
            Destroy(gameObject);
        }
    }
}