#region

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#endregion

namespace UI.FoldingThePuzzle
{
    [Serializable]
    public class GameOverUI
    {
        [SerializeField] private Image image;
        [SerializeField] private Button exitButton;
        [SerializeField] private Canvas canvas;

        public void SetImage(Texture2D texture2D)
        {
            image.sprite = Sprite.Create(
                texture2D,
                new Rect(0, 0, texture2D.width, texture2D.height),
                new Vector2(0.5f, 0.5f));
        }

        public void RegisterExitListener(UnityAction action)
        {
            exitButton.onClick.AddListener(action);
        }

        public void SetActiveFullImagePanel(bool value)
        {
            canvas.enabled = value;
        }

        public void Clear()
        {
            image.sprite = null;
            exitButton.onClick.RemoveAllListeners();
            SetActiveFullImagePanel(true);
        }
    }
}