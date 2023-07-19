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
        [SerializeField] private Button _backButton;
        [SerializeField] private CategoriesInfoScrollViewUI _categoriesScroll;
        [SerializeField] private PuzzlesInfoScrollViewUI _puzzlesScroll;

        public void OpenCategoriesPanel()
        {
            _categoriesScroll.SetActivePanel(true);
            _puzzlesScroll.SetActivePanel(false);
        }

        public void OpenPuzzleInfoPanel(string category)
        {
            _categoriesScroll.SetActivePanel(false);
            _puzzlesScroll.SetActivePanel(true);
            _puzzlesScroll.SwitchPanel(category);
        }

        public void Clear()
        {
            _backButton.onClick.RemoveAllListeners();
            _categoriesScroll.Clear();
            _puzzlesScroll.Clear();
        }

        public void AddCategoriesPanel(RectTransform[] panels) =>
            _categoriesScroll.AddPanels(panels);

        public void AddPuzzlesPanel(RectTransform[] panels, string category) =>
            _puzzlesScroll.AddPanels(panels, category);

        public void RegisterBackButtonListener(UnityAction listener) =>
            _backButton.onClick.AddListener(listener);

        public void ClearBackButtonListener() =>
            _backButton.onClick.RemoveAllListeners();
    }
}