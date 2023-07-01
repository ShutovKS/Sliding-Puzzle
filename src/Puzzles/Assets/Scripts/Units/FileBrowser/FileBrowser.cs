#region

using System.Threading;
using System.Threading.Tasks;

#endregion

namespace Units.FileBrowser
{
    public static class FileBrowser
    {
        public static async Task<string> GetFilePatchFromFileBrowser()
        {
            var filter = new[] { new SimpleFileBrowser.FileBrowser.Filter("Image", "jpg", "jpeg", "png") };

            SimpleFileBrowser.FileBrowser.SetFilters(false, filter);
            SimpleFileBrowser.FileBrowser.SetDefaultFilter(".png");
            SimpleFileBrowser.FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");

            string path = null;

            SimpleFileBrowser.FileBrowser.ShowLoadDialog(
                paths => path = paths[0],
                () => path = null,
                SimpleFileBrowser.FileBrowser.PickMode.Files,
                title: "Select Folder");

            await Task.Run(
                () =>
                {
                    while (SimpleFileBrowser.FileBrowser.IsOpen)
                        Thread.Sleep(100);
                });

            return path;
        }
    }
}