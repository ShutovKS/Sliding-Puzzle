#region

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace UI.FoldingThePuzzle
{
    [Serializable]
    public class MenuUI
    {
        public Action OnBackClicked;
        public Action OnResetClicked;

        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private Button resetButton;
        [SerializeField] private Button exitButton;

        public void Initialize()
        {
            resetButton.onClick.AddListener(() => OnResetClicked?.Invoke());
            exitButton.onClick.AddListener(() => OnBackClicked?.Invoke());
        }

        public void UpdateTimer(string text)
        {
            timerText.text = text;
        }

        public void Clear()
        {
            timerText.text = string.Empty;
        }
    }
}