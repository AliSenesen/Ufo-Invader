using System;
using _0_Scripts.Enums;
using _0_Scripts.Events;
using UnityEngine;

namespace _0_Scripts.Collectables
{
    public abstract class IPullable : MonoBehaviour
    {
        public QuestItemTypes questItemType;
        public bool isPaniced = false;

       
        
        public abstract void Pulled();
        public abstract void BreakBound();
        public abstract void Grounded();
        public abstract void Panic();
    }
}