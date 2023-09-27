using System;
using System.Collections.Generic;
using _0_Scripts.Enums;
using _0_Scripts.Events;
using _0_Scripts.GameManager;
using _0_Scripts.Level;
using _0_Scripts.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    [SerializeField] private GameObject questItemUI;
    [SerializeField] private RectTransform questParent;
    [SerializeField] private List<Sprite> questImageList;
    public List<QuestItemUI> questItemUIList;

    private void Awake()
    {
        instance = this;
    }


    public void SetQuest(LevelInfo levelInfo)
    {
        bool questItemUIExists = questParent.GetComponentInChildren<QuestItemUI>() != null;

        foreach (var Item in levelInfo.questItemsList)
        {
            if (!questItemUIExists)
            {
                QuestItemUI _questItemUI = Instantiate(questItemUI, questParent).GetComponent<QuestItemUI>();
                _questItemUI.SetQuestText(Item, questImageList[(int)Item._questItemTypes]);
                questItemUIList.Add(_questItemUI);
                
            }
           
        }
    }

    public void CollectQuest(QuestItemTypes questItemTypes)
    {
        foreach (var questItem in questItemUIList)
        {
            if (questItem.questItems._questItemTypes == questItemTypes)
            {
                questItem.DecreaseItemCount();
                return;
            }
        }
    }
}