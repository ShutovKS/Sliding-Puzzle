#region

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#endregion

namespace UI.FoldingThePuzzle
{
    public class FoldingThePuzzleGameOverUI : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Button _exitButton;
        [SerializeField] private GameObject _gameOverPanel;

        public void SetImage(Texture2D texture2D)
        {
            _image.sprite = Sprite.Create(
                texture2D,
                new Rect(0, 0, texture2D.width, texture2D.height),
                new Vector2(0.5f, 0.5f));
        }

        public void RegisterExitListener(UnityAction action)
        {
            _exitButton.onClick.AddListener(action);
        }
        
        public void SetActiveFullImagePanel(bool value)
        {
            _gameOverPanel.SetActive(value);
        }
    }
}