using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.InGameMenu
{
    public class PuzzleInformationUI : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _elementsCount;
        [SerializeField] private Button _puzzleButton;

        public void SetUp(Texture2D image, string name, int elementsCount, UnityAction listener)
        {
            _image.sprite = Sprite.Create(image, new Rect(0, 0, image.width, image.height), Vector2.zero);
            _nameText.text = name;
            _elementsCount.text = elementsCount.ToString();
            _puzzleButton.onClick.AddListener(listener);
        }
    }
}