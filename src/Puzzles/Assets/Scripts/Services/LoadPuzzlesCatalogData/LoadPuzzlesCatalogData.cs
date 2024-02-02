#region

using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Path;
using Data.PuzzleInformation;
using UnityEngine;

#endregion

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
            var images = Resources.LoadAll<Texture2D>(ResourcesPathsConstants.ALL_IMAGES);

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

        private Texture2D LoadTexture2D(string imagePath)
        {
            var texture2D = Resources.Load<Texture2D>(imagePath);
            
            return texture2D;
        }
    }
}