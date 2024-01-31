#region

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Object = UnityEngine.Object;

#endregion

namespace UI.FoldingThePuzzle
{
    [Serializable]
    public class GameplayUI
    {
        [SerializeField] private GameObject partsPanel;
        [SerializeField] private GameObject panel;

        private Dictionary<GameObject, Vector2Int> _buttonsInstanceToVectorDictionary;
        private Dictionary<Vector2Int, GameObject> _vectorToButtonsInstanceDictionary;

        public void SetActive(bool value)
        {
            panel.SetActive(value);
        }

        public void CreatedParts(int elementsAmount)
        {
            var resources = new DefaultControls.Resources();

            _vectorToButtonsInstanceDictionary =
                new Dictionary<Vector2Int, GameObject>(elementsAmount * elementsAmount);

            _buttonsInstanceToVectorDictionary =
                new Dictionary<GameObject, Vector2Int>(elementsAmount * elementsAmount);

            for (var y = 0; y < elementsAmount; y++)
            for (var x = 0; x < elementsAmount; x++)
            {
                var position = new Vector2Int(x, y);

                var buttonGo = DefaultControls.CreateButton(resources);
                buttonGo.GetComponentInChildren<Text>().enabled = false;
                buttonGo.transform.SetParent(partsPanel.transform);

                buttonGo.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.78f);

                var rectTransformComponent = buttonGo.GetComponent<RectTransform>();

                rectTransformComponent.anchorMin = new Vector2(
                    (float)position.x / elementsAmount,
                    (float)position.y / elementsAmount);

                rectTransformComponent.anchorMax = new Vector2(
                    (float)(position.x + 1) / elementsAmount,
                    (float)(position.y + 1) / elementsAmount);

                rectTransformComponent.offsetMin = new Vector2(0, 0);
                rectTransformComponent.offsetMax = new Vector2(0, 0);

                rectTransformComponent.localScale = Vector3.one;

                _vectorToButtonsInstanceDictionary.Add(position, buttonGo);
                _buttonsInstanceToVectorDictionary.Add(buttonGo, position);
            }
        }

        public void RemovePart(Vector2Int position)
        {
            if (!_vectorToButtonsInstanceDictionary.TryGetValue(position, out var partInstance)) return;

            Object.Destroy(partInstance);
            _vectorToButtonsInstanceDictionary.Remove(position);
            _buttonsInstanceToVectorDictionary.Remove(partInstance);
        }

        public void MovePart(Vector2Int oldPosition, Vector2Int newPosition, int elementsAmount)
        {
            var priceInstance = _vectorToButtonsInstanceDictionary[oldPosition];

            var rectTransform = priceInstance.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(newPosition.x, newPosition.y) / elementsAmount;

            rectTransform.anchorMax = new Vector2(newPosition.x + 1, newPosition.y + 1) / elementsAmount;

            rectTransform.offsetMin = new Vector2(0, 0);
            rectTransform.offsetMax = new Vector2(0, 0);

            _vectorToButtonsInstanceDictionary[newPosition] = priceInstance;
            _buttonsInstanceToVectorDictionary[priceInstance] = newPosition;
        }

        public void RegisteringButtonsEvents(UnityAction<Vector2Int> buttonClick)
        {
            foreach (var (_, buttonInstance) in _vectorToButtonsInstanceDictionary)
            {
                buttonInstance.GetComponent<Button>().onClick.AddListener(() =>
                    buttonClick?.Invoke(_buttonsInstanceToVectorDictionary[buttonInstance]));
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
                _vectorToButtonsInstanceDictionary[currentPosition].GetComponent<Image>().sprite = sprite;
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
                var textComponent = _vectorToButtonsInstanceDictionary[currentPosition].GetComponentInChildren<Text>();

                textComponent.enabled = true;
                textComponent.text = $"{number}";
                textComponent.resizeTextForBestFit = true;
                textComponent.resizeTextMaxSize = 240;
                number++;
            }
        }

        public void Clear()
        {
            if (_vectorToButtonsInstanceDictionary != null)
            {
                foreach (var (_, buttonInstance) in _vectorToButtonsInstanceDictionary)
                {
                    Object.Destroy(buttonInstance);
                }

                _buttonsInstanceToVectorDictionary.Clear();
                _vectorToButtonsInstanceDictionary.Clear();
            }
        }
    }
}