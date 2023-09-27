using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace _0_Scripts.UI
{
    public class UIPanelController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> UIPanelList = new();


        public void OpenPanel(UIPanels panels)
        {
            UIPanelList[(int)panels].SetActive(true);
        }

        public void ClosePanel(UIPanels panels)
        {
            UIPanelList[(int)panels].SetActive(false);
        }
    }
}