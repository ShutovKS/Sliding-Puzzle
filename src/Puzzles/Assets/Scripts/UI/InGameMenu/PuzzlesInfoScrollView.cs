using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace UI.InGameMenu
{
    [Serializable]
    public class PuzzlesInfoScrollView
    {
        public Action<int> OnPuzzleClicked;

        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private Transform contentParent;
        [SerializeField] private GameObject contentPrefab;
        [SerializeField] private GameObject puzzlePrefab;

        private readonly Dictionary<int, GameObject> _panels = new();
        private GameObject _currentPanel;

        public void OpenPanel(int id)
        {
            if (_currentPanel == null)
            {
                _currentPanel.SetActive(false);
            }
            
            if (_panels.TryGetValue(id, out var panel))
            {
                panel.SetActive(true);
                scrollRect.content = panel.GetComponent<RectTransform>();
                _currentPanel = panel;
            }
            else
            {
                throw new Exception($"Панели с id {id} не существует.");
            }
        }

        public void AddPanels(int id, params (int id, Texture2D texture)[] puzzles)
        {
            var panel = Object.Instantiate(contentPrefab, contentParent);
            scrollRect.content = panel.GetComponent<RectTransform>();
            _panels[id] = panel;
            var panelTransform = panel.transform;

            var instance = Object.Instantiate(puzzlePrefab, panelTransform);
            instance.GetComponent<PuzzleInformationUI>().SetUp(() => OnPuzzleClicked?.Invoke(-1), text: $"{id}x{id}");

            foreach (var (puzzleId, texture) in puzzles)
            {
                instance = Object.Instantiate(puzzlePrefab, panelTransform);
                instance.GetComponent<PuzzleInformationUI>().SetUp(() => OnPuzzleClicked?.Invoke(puzzleId), texture);
            }

            OpenPanel(id);
        }

        public void Clear()
        {
            foreach (var panel in _panels.Values)
            {
                Object.Destroy(panel);
            }

            _panels.Clear();
        }
    }
}