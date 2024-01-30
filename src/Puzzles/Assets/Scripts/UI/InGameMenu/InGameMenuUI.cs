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
        [field: SerializeField] public PuzzlesInfoScrollView PuzzlesScroll { get; private set; }
        [SerializeField] private Button backButton;
        [SerializeField] private Canvas canvas;

        public bool IsEnabled
        {
            get => canvas.enabled;
            set => canvas.enabled = value;
        }

        public void Clear()
        {
            backButton.onClick.RemoveAllListeners();
            PuzzlesScroll.Clear();
        }

        public void RegisterBackButtonListener(UnityAction listener)
        {
            backButton.onClick.AddListener(listener);
        }
    }
}