#region

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#endregion

namespace UI.FoldingThePuzzle
{
    public class PartsPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _partsPanel;
        [SerializeField] private GameObject _fullImagePanel;
        private Dictionary<GameObject, Vector2Int> _buttonsInstanceToVectorDictionary;

        private Dictionary<Vector2Int, GameObject> _vectorToButtonsInstanceDictionary;

        public void CreatedPiecesVisual(int elementsAmount)
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
                buttonGo.transform.SetParent(_partsPanel.transform);

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

                _vectorToButtonsInstanceDictionary.Add(position, buttonGo);
                _buttonsInstanceToVectorDictionary.Add(buttonGo, position);
            }
        }

        public void RemovePrice(Vector2Int position)
        {
            var priceInstance = _vectorToButtonsInstanceDictionary[position];

            Destroy(priceInstance);

            _vectorToButtonsInstanceDictionary.Remove(position);
            _buttonsInstanceToVectorDictionary.Remove(priceInstance);
        }

        public void FillWithPiecesOfCutsImages(Texture2D[,] textures2D, Vector2Int[,] currentsPositions)
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

        public void FillWithNumbers(Vector2Int[,] currentsPositions)
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

        public void CreatedFullImage(Texture2D texture2D)
        {
            var resources = new DefaultControls.Resources();

            var imagePanel = DefaultControls.CreatePanel(resources);
            imagePanel.transform.SetParent(_fullImagePanel.transform);

            var imageComponent = imagePanel.GetComponent<Image>();
            imageComponent.color = Color.white;
            imageComponent.sprite = Sprite.Create(
                texture2D,
                new Rect(0, 0, texture2D.width, texture2D.height),
                new Vector2(0.5f, 0.5f));


            var rectTransformComponent = imagePanel.GetComponent<RectTransform>();
            rectTransformComponent.anchorMin = new Vector2(0, 0);
            rectTransformComponent.anchorMax = new Vector2(1, 1);
            rectTransformComponent.offsetMin = new Vector2(0, 0);
            rectTransformComponent.offsetMax = new Vector2(0, 0);
        }

        public void MovePrice(Vector2Int oldPosition, Vector2Int newPosition, int elementsAmount)
        {
            var priceInstance = _vectorToButtonsInstanceDictionary[oldPosition];

            var rectTransform = priceInstance.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(
                (float)newPosition.x / elementsAmount,
                (float)newPosition.y / elementsAmount);

            rectTransform.anchorMax = new Vector2(
                (float)(newPosition.x + 1) / elementsAmount,
                (float)(newPosition.y + 1) / elementsAmount);

            rectTransform.offsetMin = new Vector2(0, 0);
            rectTransform.offsetMax = new Vector2(0, 0);

            _vectorToButtonsInstanceDictionary[newPosition] = priceInstance;
            _buttonsInstanceToVectorDictionary[priceInstance] = newPosition;
        }

        public void RegisteringButtonsEvents(UnityAction<Vector2Int> buttonClick)
        {
            foreach (var (_, buttonInstance) in _vectorToButtonsInstanceDictionary)
            {
                buttonInstance.GetComponent<Button>().onClick.AddListener(
                    () => buttonClick?.Invoke(_buttonsInstanceToVectorDictionary[buttonInstance]));
            }
        }

        public void PartsPanelSetActive(bool value)
        {
            _partsPanel.SetActive(value);
        }

        public void FullImagePanelSetActive(bool value)
        {
            _fullImagePanel.SetActive(value);
        }
    }
}