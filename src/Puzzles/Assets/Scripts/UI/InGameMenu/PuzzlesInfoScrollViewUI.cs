using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InGameMenu
{
    public class PuzzlesInfoScrollViewUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _scrollViewportRT;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private GameObject _panel;

        private readonly Dictionary<string, GameObject> _panels = new();
        private GameObject _currentPanel;

        public void AddPanels(RectTransform[] panels, string categoriesId)
        {
            if (!_panels.TryGetValue(categoriesId, out var categoryInformationPanel))
            {
                var resources = new DefaultControls.Resources();
                categoryInformationPanel = DefaultControls.CreatePanel(resources);
                categoryInformationPanel.GetComponent<Image>().enabled = false;
                categoryInformationPanel.transform.SetParent(_scrollViewportRT, false);

                var informationPanelRectTransform = categoryInformationPanel.GetComponent<RectTransform>();
                informationPanelRectTransform.anchorMin = new Vector2(0, 1);
                informationPanelRectTransform.anchorMax = new Vector2(1, 1);
                informationPanelRectTransform.pivot = new Vector2(0, 1);
                informationPanelRectTransform.sizeDelta = new Vector2(0, 0);
                informationPanelRectTransform.anchoredPosition = new Vector2(0, 0);
            }

            categoryInformationPanel.SetActive(true);

            foreach (var panel in panels)
            {
                panel.SetParent(categoryInformationPanel.transform, false);
                var informationPanelRectTransform = categoryInformationPanel.GetComponent<RectTransform>();

                var currentWidth = informationPanelRectTransform.rect.width;
                var panelSizeDelta = panel.sizeDelta;
                var panelsPerRow = (int)(currentWidth * (1f / panelSizeDelta.x));
                var indentX = (currentWidth - (panelSizeDelta.x * panelsPerRow)) * (1f / (panelsPerRow - 1));

                var childCount = categoryInformationPanel.transform.childCount;
                var rowCount = Mathf.CeilToInt(childCount * (1f / panelsPerRow)) - 1;
                var column = (childCount - 1) % panelsPerRow;

                var panelX = panelSizeDelta.x * (0.5f + column) + indentX * column;
                var panelY = -panelSizeDelta.y * (rowCount * 1.1f + 0.5f);

                panel.anchoredPosition = new Vector2(panelX, panelY);

                var totalHeight = (rowCount + 1) * (panel.sizeDelta.y * (1 + rowCount * 0.05f));

                var parentPanelSizeDelta = informationPanelRectTransform.sizeDelta;
                informationPanelRectTransform.sizeDelta = new Vector2(parentPanelSizeDelta.x, totalHeight);
            }

            _panels[categoriesId] = categoryInformationPanel;
            categoryInformationPanel.SetActive(false);
        }

        public void SwitchPanel(string categoriesId)
        {
            if (_currentPanel != null)
                _currentPanel.SetActive(false);

            if (!_panels.TryGetValue(categoriesId, out var panel))
                throw new Exception("Panel not found");

            panel.SetActive(true);
            _currentPanel = panel;
            _scrollRect.content = panel.GetComponent<RectTransform>();
        }
        
        public void SetActivePanel(bool isActive)
        {
            _panel.SetActive(isActive);
        }

        public void Clear()
        {
            foreach (var panel in _panels.Values)
            {
                Destroy(panel);
            }

            _panels.Clear();
        }
    }
}