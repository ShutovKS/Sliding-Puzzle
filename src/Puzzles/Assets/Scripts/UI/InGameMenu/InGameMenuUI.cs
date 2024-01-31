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
        public Action OnBackClicked;
        [field: SerializeField] public PuzzlesInfoScrollView PuzzlesScroll { get; private set; }
        [field: SerializeField] public NumberParts NumberParts { get; private set; }
        [SerializeField] private Button backButton;
        [SerializeField] private Canvas canvas;

        private void Awake()
        {
            backButton.onClick.AddListener(() => OnBackClicked?.Invoke());
            NumberParts.Initialize();
        }

        public bool IsEnabled
        {
            get => canvas.enabled;
            set => canvas.enabled = value;
        }
    }
}