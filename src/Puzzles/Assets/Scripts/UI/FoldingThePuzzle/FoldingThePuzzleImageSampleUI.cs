#region

using UnityEngine;
using UnityEngine.UI;

#endregion

namespace UI.FoldingThePuzzle
{
    public class FoldingThePuzzleImageSampleUI : MonoBehaviour
    {
        [SerializeField] private Image _image;

        public void SetImageSample(Texture2D texture2D)
        {
            _image.sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.zero);
        }
        
        public void Clear()
        {
            _image.sprite = null;
        }
    }
}