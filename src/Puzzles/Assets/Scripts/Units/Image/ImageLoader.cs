#region

using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SimpleFileBrowser;
using UnityEngine;

#endregion

namespace Units.Image
{
    public static class ImageLoader
    {
        public static async Task<string> GetFilePatchFromFileBrowser()
        {
            var filter = new[] { new FileBrowser.Filter("Image", "jpg", "jpeg", "png") };

            FileBrowser.SetFilters(false, filter);
            FileBrowser.SetDefaultFilter(".png");
            FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");

            string path = null;

            FileBrowser.ShowLoadDialog(
                paths => path = paths[0],
                () => path = null,
                FileBrowser.PickMode.Files,
                title: "Select Folder");

            await Task.Run(
                () =>
                {
                    while (FileBrowser.IsOpen)
                        Thread.Sleep(100);
                });

            return path;
        }

        public static Texture2D GetImageByPath(string imagePath)
        {
            var file = File.ReadAllBytes(imagePath);

            var loadImage = new Texture2D(2, 2);

            loadImage.LoadImage(file);

            return loadImage;
        }
    }
}