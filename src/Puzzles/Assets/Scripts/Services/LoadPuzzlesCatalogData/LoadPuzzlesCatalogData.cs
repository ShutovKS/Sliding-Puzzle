using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Data.PuzzleInformation;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Services.LoadPuzzlesCatalogData
{
    public class LoadPuzzlesCatalogData : ILoadPuzzlesCatalogData
    {
        private readonly Dictionary<string, PuzzleInformation> _puzzleInformation = new();

        public LoadPuzzlesCatalogData()
        {
            LoadData();
        }

        public void LoadData()
        {
            var images = Resources.LoadAll<Texture2D>("Images");

            foreach (var image in images)
            {
                var puzzleInformation = new PuzzleInformation($"{image.GetInstanceID()}", image);
                _puzzleInformation.Add(puzzleInformation.Id, puzzleInformation);
            }
        }

        public List<PuzzleInformation> GetPuzzlesInformation()
        {
            return new List<PuzzleInformation>(_puzzleInformation.Values);
        }

        private async Task<Texture2D> LoadTexture2D(string imagePath)
        {
            var handle = Addressables.LoadAssetAsync<Texture2D>(imagePath);
            await handle.Task;

            var image = handle.Result;

            return image;
        }
    }
}