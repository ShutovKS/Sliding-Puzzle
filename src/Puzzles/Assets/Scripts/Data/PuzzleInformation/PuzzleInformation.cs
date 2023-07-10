#region

using UnityEngine;

#endregion

namespace Data.PuzzleInformation
{
    public struct PuzzleInformation
    {
        public readonly Texture2D Image;
        public readonly int ElementsCount;
        public readonly string Name;
        public readonly string Id;

        public PuzzleInformation(Texture2D image, int elementsCount, string name, string id)
        {
            Image = image;
            ElementsCount = elementsCount;
            Name = name;
            Id = id;
        }
    }
}