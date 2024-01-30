using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace UI.InGameMenu
{
    [Serializable]
    public class CategoriesInfoScrollView
    {
        [SerializeField] private RectTransform scrollViewportRectTransform;
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private Canvas canvas;

        private GameObject _currentPanel;
        private const float INDENT_BETWEEN_PANELS = 10f;

        public void AddPanels(IEnumerable<RectTransform> panels)
        {
            RectTransform contentPanelRT;
            if (_currentPanel == null)
            {
                var resources = new DefaultControls.Resources();
                _currentPanel = DefaultControls.CreatePanel(resources);
                _currentPanel.GetComponent<Image>().enabled = false;
                _currentPanel.transform.SetParent(scrollViewportRectTransform, false);

                contentPanelRT = _currentPanel.GetComponent<RectTransform>();
                contentPanelRT.anchorMin = new Vector2(0, 0.5f);
                contentPanelRT.anchorMax = new Vector2(0, 0.5f);
                contentPanelRT.pivot = new Vector2(0, 1);
                contentPanelRT.sizeDelta = new Vector2(0, 0);
                contentPanelRT.anchoredPosition = new Vector2(0, 0);
                
                scrollRect.content = contentPanelRT;
            }
            else
            {
                contentPanelRT = _currentPanel.GetComponent<RectTransform>();
            }

            foreach (var rectTransform in panels)
            {
                rectTransform.SetParent(contentPanelRT, false);

                var scrollSizeDelta = contentPanelRT.sizeDelta;
                scrollSizeDelta.x += contentPanelRT.childCount == 1
                    ? rectTransform.sizeDelta.x
                    : rectTransform.sizeDelta.x + INDENT_BETWEEN_PANELS;

                contentPanelRT.sizeDelta = scrollSizeDelta;

                var panelAnchoredPosition = rectTransform.anchoredPosition;
                panelAnchoredPosition.x = scrollSizeDelta.x - rectTransform.sizeDelta.x * 0.5f;

                rectTransform.anchoredPosition = panelAnchoredPosition;
            }
        }

        public void SetActivePanel(bool isActive)
        {
            canvas.enabled = isActive;
        }

        public void Clear()
        {
            if (_currentPanel != null)
            {
                Object.Destroy(_currentPanel);
                _currentPanel = null;
            }
        }
    }
}