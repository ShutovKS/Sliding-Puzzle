#region

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace UI.MainMenu
{
    public class MainMenuUI : MonoBehaviour
    {
        [Header("Properties")] public Action<int> OnLevelClicked;
        public Action OnInstructionsClicked;
        public Action OnExitClicked;

        public bool IsEnabled
        {
            get => canvas.enabled;
            set => canvas.enabled = value;
        }

        [Header("UI")] [field: SerializeField] public Instructions instructions;
        [SerializeField] private Canvas canvas;
        [SerializeField] private Button instructionsButton;
        [SerializeField] private Button exitButton;

        [Header("Levels")] [SerializeField] private GameObject levelPrefab;
        [SerializeField] private Transform levelsContent;
        [SerializeField] [Range(1, 9)] private int levelCount = 8;
        [SerializeField] private int startRangeLevel = 2;
        private const int COLUMN_COUNT = 3;
        private const float INDENTATION_LEVEL_BUTTON = 0.025f;
        private const float SIZE_LEVEL_BUTTON = 0.3f;

        private void Awake()
        {
            var index = 0;
            instructionsButton.onClick.AddListener(() => OnInstructionsClicked?.Invoke());
            exitButton.onClick.AddListener(() => OnExitClicked?.Invoke());

#if UNITY_WEBGL
            exitButton.gameObject.SetActive(false);
#endif

            for (var number = startRangeLevel; number < startRangeLevel + levelCount; number++)
            {
                var numberLevel = number;
                CreatedLevelButton(index, $"{number}x{number}", () => OnLevelClicked?.Invoke(numberLevel));
                index++;
            }

            CreatedLevelButton(index, "Image", () => OnLevelClicked?.Invoke(0));

            instructions.Initialize();
        }

        private void CreatedLevelButton(int index, string name, Action OnClicked)
        {
            var line = index / COLUMN_COUNT;
            var column = index - line * COLUMN_COUNT;
            var width = INDENTATION_LEVEL_BUTTON * (1 + column) + SIZE_LEVEL_BUTTON * column;
            var height = 1f - (INDENTATION_LEVEL_BUTTON * (1 + line) + SIZE_LEVEL_BUTTON * line);

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
            button.onClick.AddListener(() => OnClicked?.Invoke());
        }
    }

    [Serializable]
    public class Instructions
    {
        public Action OnCloseClicked;

        [SerializeField] private GameObject panel;
        [SerializeField] private Button closeButton;

        public void Initialize()
        {
            closeButton.onClick.AddListener(() => OnCloseClicked?.Invoke());
        }

        public void SetActive(bool isActive)
        {
            panel.SetActive(isActive);
        }
    }
}