#region

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#endregion

namespace UI.MainMenu
{
    public class MainMenuUI : MonoBehaviour
    {
        [Header("Action")] 
        public Action<int> OnLevelClicked;
        public Action OnInstructionsClicked;
        public Action OnExitClicked;

        [Header("UI")] 
        [SerializeField] private Canvas canvas;
        [SerializeField] private Button instructionsButton;
        [SerializeField] private Button exitButton;

        [Header("Levels")] 
        [SerializeField] private GameObject levelPrefab;
        [SerializeField] private Transform levelsContent;
        [SerializeField, Range(1, 9)] private int levelCount = 8;
        [SerializeField] private int startRangeLevel = 2;
        private const int COLUMN_COUNT = 3;
        private const float INDENTATION_LEVEL_BUTTON = 0.025f;
        private const float SIZE_LEVEL_BUTTON = 0.3f;

        private void Awake()
        {
            var index = 0;
            instructionsButton.onClick.AddListener(() => OnInstructionsClicked?.Invoke());
            exitButton.onClick.AddListener(() => OnExitClicked?.Invoke());

            for (var number = startRangeLevel; number < startRangeLevel + levelCount; number++)
            {
                var line = index / COLUMN_COUNT;
                var column = index - line * COLUMN_COUNT;
                var width = INDENTATION_LEVEL_BUTTON * (1 + column) + SIZE_LEVEL_BUTTON * column;
                var height = 1f - (INDENTATION_LEVEL_BUTTON * (1 + line) + SIZE_LEVEL_BUTTON * line);
                var name = $"{startRangeLevel + index}x{startRangeLevel + index}";

                var levelButton = Instantiate(levelPrefab, levelsContent);
                levelButton.SetActive(true);
                levelButton.name = name;

                var rectTransform = levelButton.GetComponent<RectTransform>();
                rectTransform.anchorMin = new Vector2(width, height - SIZE_LEVEL_BUTTON);
                rectTransform.anchorMax = new Vector2(width + SIZE_LEVEL_BUTTON, height);
                rectTransform.offsetMin = Vector2.zero;
                rectTransform.offsetMax = Vector2.zero;

                var textMeshProUGUI = levelButton.GetComponentInChildren<TextMeshProUGUI>();
                textMeshProUGUI.text = name;

                var button = levelButton.GetComponent<Button>();
                var numberForAction = number;
                button.onClick.AddListener(() => OnLevelClicked?.Invoke(numberForAction));

                index++;
            }
        }
    }
}