#region

using System;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace UI.InGameMenu
{
    public class InGameMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private RectTransform _scrollContentRectTransform;
        private readonly float _indentBetweenPanels = 10f;

        public void RegisterBackButtonListener(Action listener)
        {
            _backButton.onClick.AddListener(() => listener?.Invoke());
        }

        public void AddPanelToScroll(RectTransform panel)
        {
            panel.SetParent(_scrollContentRectTransform, false);

            var scrollSizeDelta = _scrollContentRectTransform.sizeDelta;
            scrollSizeDelta.x += _scrollContentRectTransform.childCount == 1
                ? panel.sizeDelta.x
                : panel.sizeDelta.x + _indentBetweenPanels;

            _scrollContentRectTransform.sizeDelta = scrollSizeDelta;

            var panelAnchoredPosition = panel.anchoredPosition;
            panelAnchoredPosition.x = scrollSizeDelta.x - panel.sizeDelta.x * 0.5f;

            panel.anchoredPosition = panelAnchoredPosition;
        }
    }
}