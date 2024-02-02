#region

using System;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace UI.FoldingThePuzzle
{
    [Serializable]
    public class ImageSample
    {
        [SerializeField] private Image image;

        public void SetImageSample(Texture2D texture2D)
        {
            image.enabled = texture2D != null;
            image.sprite = texture2D != null
                ? Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.zero)
                : null;
        }
    }
}