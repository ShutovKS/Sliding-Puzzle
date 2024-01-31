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
        [SerializeField] private GameObject panel;

        public void SetImage(Texture2D texture2D)
        {
            image.sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.one / 2);
        }

        public void RegisterExitListener(UnityAction action)
        {
            exitButton.onClick.AddListener(action);
        }

        public void SetActiveFullImagePanel(bool value)
        {
            panel.SetActive(value);
        }

        public void Clear()
        {
            image.sprite = null;
            exitButton.onClick.RemoveAllListeners();
            SetActiveFullImagePanel(true);
        }
    }
}