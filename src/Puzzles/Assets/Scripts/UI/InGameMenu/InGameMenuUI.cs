#region

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

#endregion

namespace UI.InGameMenu
{
    public class InGameMenuUI : MonoBehaviour
    {
        [SerializeField] private Button backButton;
        [SerializeField] private CategoriesInfoScrollView categoriesScroll;
        [SerializeField] private PuzzlesInfoScrollView puzzlesScroll;

        public void OpenCategoriesPanel()
        {
            categoriesScroll.SetActivePanel(true);
            puzzlesScroll.SetActivePanel(false);
        }

        public void OpenPuzzleInfoPanel(string category)
        {
            categoriesScroll.SetActivePanel(false);
            puzzlesScroll.SetActivePanel(true);
            puzzlesScroll.SwitchPanel(category);
        }

        public void Clear()
        {
            backButton.onClick.RemoveAllListeners();
            categoriesScroll.Clear();
            puzzlesScroll.Clear();
        }

        public void AddCategoriesPanel(RectTransform[] panels)
        {
            categoriesScroll.AddPanels(panels);
        }

        public void AddPuzzlesPanel(RectTransform[] panels, string category)
        {
            puzzlesScroll.AddPanels(panels, category);
        }

        public void RegisterBackButtonListener(UnityAction listener)
        {
            backButton.onClick.AddListener(listener);
        }

        public void ClearBackButtonListener()
        {
            backButton.onClick.RemoveAllListeners();
        }
    }
}