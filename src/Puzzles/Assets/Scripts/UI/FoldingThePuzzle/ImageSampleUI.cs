#region

using System;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace UI.FoldingThePuzzle
{
    [Serializable]
    public class ImageSampleUI
    {
        [SerializeField] private Image image;

        public void SetImageSample(Texture2D texture2D)
        {
            image.sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.zero);
        }
        
        public void Clear()
        {
            image.sprite = null;
        }
    }
}