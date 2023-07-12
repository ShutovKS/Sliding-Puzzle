using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.InGameMenu
{
    public class CategoryInformationUI : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private Button _buttonClick;

        public void SetUp(Texture2D image, string name, UnityAction listener)
        {
            _image.sprite = Sprite.Create(image, new Rect(0, 0, image.width, image.height), Vector2.zero);
            _nameText.text = name;
            _buttonClick.onClick.AddListener(listener);
        }
    }
}