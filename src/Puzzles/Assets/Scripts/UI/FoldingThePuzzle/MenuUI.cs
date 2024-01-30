#region

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#endregion

namespace UI.FoldingThePuzzle
{
    [Serializable]
    public class MenuUI
    {
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private Button resetButton;
        [SerializeField] private Button exitButton;

        public void RegisterResetListener(UnityAction action)
        {
            resetButton.onClick.AddListener(action);
        }

        public void RegisterExitListener(UnityAction action)
        {
            exitButton.onClick.AddListener(action);
        }

        public void UpdateTimer(string text)
        {
            timerText.text = text;
        }

        public void Clear()
        {
            timerText.text = string.Empty;
            resetButton.onClick.RemoveAllListeners();
            exitButton.onClick.RemoveAllListeners();
        }
    }
}