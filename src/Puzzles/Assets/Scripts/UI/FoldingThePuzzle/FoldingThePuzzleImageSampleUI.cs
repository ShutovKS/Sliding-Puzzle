using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoldingThePuzzleImageSampleUI : MonoBehaviour
{
    [SerializeField] private Image _image;

    public void SetImageSample(Texture2D texture2D)
    {
        _image.sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.zero);
    }
}