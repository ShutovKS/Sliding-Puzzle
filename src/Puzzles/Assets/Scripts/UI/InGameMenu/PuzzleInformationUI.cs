#region

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#endregion

namespace UI.InGameMenu
{
    public class PuzzleInformationUI : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private Button puzzleButton;

        public void SetUp(UnityAction listener, Texture2D texture = null, string text = "")
        {
            puzzleButton.onClick.AddListener(listener);

            if (texture != null)
            {
                image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            }

            if (text != "")
            {
                nameText.text = text;
            }
        }
    }
}