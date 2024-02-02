#region

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace UI.InGameMenu
{
    [Serializable]
    public class NumberParts
    {
        public Action<int> OnCompleteClicked;
        public Action OnBackClicked;

        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI currentNumberText;
        [SerializeField] private Button completeButton;
        [SerializeField] private Button backButton;
        [SerializeField] private GameObject panel;

        public void Initialize()
        {
            SetNumber();

            slider.onValueChanged.AddListener(_ => SetNumber());
            completeButton.onClick.AddListener(() => OnCompleteClicked?.Invoke((int)slider.value));
            backButton.onClick.AddListener(() => OnBackClicked?.Invoke());
        }

        public void SetActive(bool value)
        {
            SetNumber();
            panel.SetActive(value);
        }

        private void SetNumber()
        {
            currentNumberText.text = $"{slider.value}";
        }
    }
}