using UnityEngine;
using UnityEngine.UI;

namespace UI.InGameMenu
{
    public class InGameMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private RectTransform _scrollContentRectTransform;

        public void RegisterBackButtonListener(System.Action listener)
        {
            _backButton.onClick.AddListener(() => listener?.Invoke());
        }

        public void AddPanelToScroll(RectTransform panel)
        {
            panel.SetParent(_scrollContentRectTransform, false);

            var scrollSizeDelta = _scrollContentRectTransform.sizeDelta;
            scrollSizeDelta.x += panel.sizeDelta.x;
            _scrollContentRectTransform.sizeDelta = scrollSizeDelta;

            var panelAnchoredPosition = panel.anchoredPosition;
            panelAnchoredPosition.x = scrollSizeDelta.x - panel.sizeDelta.x;
            panel.anchoredPosition = panelAnchoredPosition;
        }
    }
}