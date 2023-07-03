#region

using UnityEngine;
using UnityEngine.UI;

#endregion

namespace UI.FoldingThePuzzle
{
    public class FullImageUI : MonoBehaviour
    {
        [SerializeField] private GameObject _fullImagePanel;

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

        public void PanelSetActive(bool value)
        {
            _fullImagePanel.SetActive(value);
        }
    }
}