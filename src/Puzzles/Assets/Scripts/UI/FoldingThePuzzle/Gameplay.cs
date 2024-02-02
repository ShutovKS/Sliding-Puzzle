#region

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Object = UnityEngine.Object;

#endregion

namespace UI.FoldingThePuzzle
{
    [Serializable]
    public class Gameplay
    {
        [SerializeField] private GameObject partsPanel;
        [SerializeField] private GameObject panel;
        [SerializeField] private GameObject cellPrefab;

        private Dictionary<CellUI, Vector2Int> _cellsInstanceToVectorDictionary = new();
        private Dictionary<Vector2Int, CellUI> _vectorToCellsInstanceDictionary = new();

        public void SetActive(bool value)
        {
            panel.SetActive(value);
        }

        public void CreatedParts(int elementsAmount)
        {
            for (var y = 0; y < elementsAmount; y++)
            for (var x = 0; x < elementsAmount; x++)
            {
                var instantiate = Object.Instantiate(cellPrefab, partsPanel.transform);
                instantiate.SetActive(true);
                var cellUI = instantiate.GetComponent<CellUI>();

                cellUI.Image.color = new Color(1f, 1f, 1f, 0.78f);

                var rectTransformComponent = cellUI.RectTransform;

                var position = new Vector2Int(x, y);
                rectTransformComponent.anchorMin = new Vector2(
                    (float)position.x / elementsAmount,
                    (float)position.y / elementsAmount);

                rectTransformComponent.anchorMax = new Vector2(
                    (float)(position.x + 1) / elementsAmount,
                    (float)(position.y + 1) / elementsAmount);

                rectTransformComponent.offsetMin = new Vector2(0, 0);
                rectTransformComponent.offsetMax = new Vector2(0, 0);

                rectTransformComponent.localScale = Vector3.one;

                try // Почему то иногда возникает ошибка, повторяется позиция и выдаёт исключение, что такой ключ уже есть (1 из 10 попыток)
                {
                    _cellsInstanceToVectorDictionary.Add(cellUI, position);
                    _vectorToCellsInstanceDictionary.Add(position, cellUI);
                }
                catch (Exception e) // Костыль: просто перезапускае метод
                {
                    Debug.Log(e);
                    Object.Destroy(instantiate);
                    Clear();
                    CreatedParts(elementsAmount);
                    return;
                }
            }
        }

        public void RemovePart(Vector2Int position)
        {
            if (!_vectorToCellsInstanceDictionary.TryGetValue(position, out var cellUI))
            {
                return;
            }

            Object.Destroy(cellUI.GameObject);

            _vectorToCellsInstanceDictionary.Remove(position);
            _cellsInstanceToVectorDictionary.Remove(cellUI);
        }

        public void MovePart(Vector2Int oldPosition, Vector2Int newPosition, int elementsAmount)
        {
            var priceInstance = _vectorToCellsInstanceDictionary[oldPosition];

            var rectTransform = priceInstance.RectTransform;

            rectTransform.anchorMin = new Vector2(newPosition.x, newPosition.y) / elementsAmount;
            rectTransform.anchorMax = new Vector2(newPosition.x + 1, newPosition.y + 1) / elementsAmount;

            rectTransform.offsetMin = new Vector2(0, 0);
            rectTransform.offsetMax = new Vector2(0, 0);

            _vectorToCellsInstanceDictionary[newPosition] = priceInstance;
            _cellsInstanceToVectorDictionary[priceInstance] = newPosition;
        }

        public void RegisteringButtonsEvents(UnityAction<Vector2Int> buttonClick)
        {
            foreach (var (_, cellUI) in _vectorToCellsInstanceDictionary)
            {
                cellUI.Button.onClick.AddListener(() => buttonClick?.Invoke(_cellsInstanceToVectorDictionary[cellUI]));
            }
        }

        public void FillWithPartsOfCutsImages(Texture2D[,] textures2D, Vector2Int[,] currentsPositions)
        {
            var elementsAmount = currentsPositions.GetLength(0);

            for (var y = 0; y < elementsAmount; y++)
            for (var x = 0; x < elementsAmount; x++)
            {
                var texture = textures2D[x, y];
                var sprite = Sprite.Create(
                    texture,
                    new Rect(0, 0, texture.width, texture.height),
                    new Vector2(0.5f, 0.5f));

                var currentPosition = currentsPositions[y, x];
                _vectorToCellsInstanceDictionary[currentPosition].Image.sprite = sprite;
            }
        }

        public void FillWithPartsOfCutsNumbers(Vector2Int[,] currentsPositions)
        {
            var elementsAmount = currentsPositions.GetLength(0);

            var number = 1;
            for (var y = elementsAmount - 1; y >= 0; y--)
            for (var x = 0; x < elementsAmount; x++)
            {
                var currentPosition = currentsPositions[y, x];
                _vectorToCellsInstanceDictionary[currentPosition].Text.text = $"{number}";
                number++;
            }
        }

        public void Clear()
        {
            if (_vectorToCellsInstanceDictionary != null)
            {
                foreach (var (_, buttonInstance) in _vectorToCellsInstanceDictionary)
                {
                    Object.Destroy(buttonInstance.GameObject);
                }

                _cellsInstanceToVectorDictionary.Clear();
                _vectorToCellsInstanceDictionary.Clear();

                _cellsInstanceToVectorDictionary = new Dictionary<CellUI, Vector2Int>();
                _vectorToCellsInstanceDictionary = new Dictionary<Vector2Int, CellUI>();
            }
        }
    }
}