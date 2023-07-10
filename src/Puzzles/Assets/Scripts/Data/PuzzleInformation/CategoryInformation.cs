using UnityEngine;

namespace Data.PuzzleInformation
{
    public struct CategoryInformation
    {
        public readonly Texture2D Image;
        public readonly int PuzzleCount;
        public readonly string Name;
        public readonly string Id;
        
        public CategoryInformation(Texture2D image, int puzzleCount, string name, string id)
        {
            Image = image;
            PuzzleCount = puzzleCount;
            Name = name;
            Id = id;
        }
    }
}