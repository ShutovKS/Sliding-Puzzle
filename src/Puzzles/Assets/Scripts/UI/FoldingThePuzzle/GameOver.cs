#region

using System;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace UI.FoldingThePuzzle
{
    [Serializable]
    public class GameOver
    {
        public Action OnBackClicked;

        [SerializeField] private Image image;
        [SerializeField] private Button exitButton;
        [SerializeField] private GameObject panel;

        public void Initialize()
        {
            exitButton.onClick.AddListener(() => OnBackClicked?.Invoke());
        }

        public void SetImage(Texture2D texture2D)
        {
            image.enabled = texture2D != null;
            image.sprite = texture2D != null
                ? Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f))
                : null;
        }

        public void SetActive(bool value)
        {
            panel.SetActive(value);
        }
    }
}