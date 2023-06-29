#region

using System.IO;
using UnityEngine;

#endregion

namespace Units.Image
{
    public static class ImageLoader
    {
        public static Texture2D GetImageByPath(string imagePath)
        {
            var file = File.ReadAllBytes(imagePath);

            var loadImage = new Texture2D(2, 2);

            loadImage.LoadImage(file);

            return loadImage;
        }
    }
}