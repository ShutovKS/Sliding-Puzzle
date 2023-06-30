using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using System.Linq;

namespace UI.FoldingThePuzzle
{
    public class PartsPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _partsPanel;
        [SerializeField] private GameObject _fullImagePanel;

        private Dictionary<Vector2Int, GameObject> _vectorToButtonsInstanceDictionary;
        private Dictionary<GameObject, Vector2Int> _buttonsInstanceToVectorDictionary;

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

                var rectTransform = buttonGo.GetComponent<RectTransform>();

                rectTransform.anchorMin = new Vector2(
                    (float)position.x / elementsAmount,
                    (float)position.y / elementsAmount);

                rectTransform.anchorMax = new Vector2(
                    (float)(position.x + 1) / elementsAmount,
                    (float)(position.y + 1) / elementsAmount);

                rectTransform.offsetMin = new Vector2(0, 0);
                rectTransform.offsetMax = new Vector2(0, 0);

                _vectorToButtonsInstanceDictionary.Add(position, buttonGo);
                _buttonsInstanceToVectorDictionary.Add(buttonGo, position);
            }
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

        public void RemovePrice(Vector2Int position)
        {
            var priceInstance = _vectorToButtonsInstanceDictionary[position];

            Destroy(priceInstance);

            _vectorToButtonsInstanceDictionary.Remove(position);
            _buttonsInstanceToVectorDictionary.Remove(priceInstance);
        }

        public void FillWithPiecesOfCutsImages(Texture2D[,] textures2D, Vector2Int[,] currentsPositions)
        {
            var elementsAmount = textures2D.GetLength(0);

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

        public void PartsPanelSetActive(bool value)
        {
            _partsPanel.SetActive(value);
        }

        public void FullImagePanelSetActive(bool value)
        {
            _fullImagePanel.SetActive(value);
        }

        public void RegisteringButtonsEvents(UnityAction<Vector2Int> buttonClick)
        {
            foreach (var (position, buttonInstance) in _vectorToButtonsInstanceDictionary)
            {
                buttonInstance.GetComponent<Button>().onClick.AddListener(
                    () => buttonClick?.Invoke(_buttonsInstanceToVectorDictionary[buttonInstance]));
            }
        }

        public void CreatedFullImage(Texture2D texture2D)
        {
            var resources = new DefaultControls.Resources();

            var imagePanel = DefaultControls.CreatePanel(resources);
            var image = imagePanel.GetComponent<Image>();
            image.sprite = Sprite.Create(
                texture2D,
                new Rect(0, 0, texture2D.width, texture2D.height),
                new Vector2(0.5f, 0.5f));

            imagePanel.transform.SetParent(_fullImagePanel.transform);
            imagePanel.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            imagePanel.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            imagePanel.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            imagePanel.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        }
    }
}