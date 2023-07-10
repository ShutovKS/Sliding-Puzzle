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
        private List<CategoryInformation> _categoriesInformations;
        private Dictionary<string, List<PuzzleInformation>> _puzzleDictionary;
        
        public LoadPuzzlesCatalogData()
        {
            LoadData();
        }

        public async void LoadData()
        {
            var path = Path.Combine(Application.streamingAssetsPath, "PuzzlesCatalogData.json");
            var json = File.ReadAllText(path);

            var catalogData = JsonUtility.FromJson<PuzzlesCatalogData>(json);
            _puzzleDictionary = new Dictionary<string, List<PuzzleInformation>>();

            if (catalogData.categories == null) return;

            _categoriesInformations = new List<CategoryInformation>();
            foreach (var category in catalogData.categories)
            {
                var categoryInformation = new CategoryInformation(
                    await LoadTexture2D(category.image_path),
                    category.puzzles.Count,
                    category.name,
                    category.id);

                _categoriesInformations.Add(categoryInformation);

                var puzzleInformations = new List<PuzzleInformation>();
                foreach (var puzzle in category.puzzles)
                {
                    var puzzleInformation = new PuzzleInformation(
                        await LoadTexture2D(puzzle.image_path),
                        puzzle.element_count,
                        puzzle.name,
                        puzzle.id);
                    
                    puzzleInformations.Add(puzzleInformation);
                }

                _puzzleDictionary.Add(category.id, puzzleInformations);
            }
        }

        public List<PuzzleInformation> GetPuzzlesInformations(string categoryId)
        {
            return _puzzleDictionary.TryGetValue(categoryId, out var puzzleInformations)
                ? puzzleInformations
                : null;
        }

        public List<CategoryInformation> GetCategoriesInformations()
        {
            return _categoriesInformations;
        }


        private async Task<Texture2D> LoadTexture2D(string imagePath)
        {
            var handle = Addressables.LoadAssetAsync<Texture2D>(imagePath);
            await handle.Task;
    
            var image = handle.Result;
    
            Debug.Log($"Image {image == null}, Path {imagePath}");

            return image;
        }
    }
}