using UnityEngine;
using UnityEngine.UI;

namespace UI.InGameMenu
{
    public class CategoriesInfoScrollViewUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _scrollViewportRT;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private GameObject _panel;

        private GameObject _currentPanel;
        private readonly float _indentBetweenPanels = 10f;

        public void AddPanels(RectTransform[] panels)
        {
            RectTransform contentPanelRT;
            if (_currentPanel == null)
            {
                var resources = new DefaultControls.Resources();
                _currentPanel = DefaultControls.CreatePanel(resources);
                _currentPanel.GetComponent<Image>().enabled = false;
                _currentPanel.transform.SetParent(_scrollViewportRT, false);

                contentPanelRT = _currentPanel.GetComponent<RectTransform>();
                contentPanelRT.anchorMin = new Vector2(0, 0.5f);
                contentPanelRT.anchorMax = new Vector2(0, 0.5f);
                contentPanelRT.pivot = new Vector2(0, 1);
                contentPanelRT.sizeDelta = new Vector2(0, 0);
                contentPanelRT.anchoredPosition = new Vector2(0, 0);
                
                _scrollRect.content = contentPanelRT;
            }
            else
            {
                contentPanelRT = _currentPanel.GetComponent<RectTransform>();
            }

            foreach (var panel in panels)
            {
                panel.SetParent(contentPanelRT, false);

                var scrollSizeDelta = contentPanelRT.sizeDelta;
                scrollSizeDelta.x += contentPanelRT.childCount == 1
                    ? panel.sizeDelta.x
                    : panel.sizeDelta.x + _indentBetweenPanels;

                contentPanelRT.sizeDelta = scrollSizeDelta;

                var panelAnchoredPosition = panel.anchoredPosition;
                panelAnchoredPosition.x = scrollSizeDelta.x - panel.sizeDelta.x * 0.5f;

                panel.anchoredPosition = panelAnchoredPosition;
            }
        }

        public void SetActivePanel(bool isActive)
        {
            _panel.SetActive(isActive);
        }

        public void Clear()
        {
            if (_currentPanel != null)
            {
                Destroy(_currentPanel);
                _currentPanel = null;
            }
        }
    }
}