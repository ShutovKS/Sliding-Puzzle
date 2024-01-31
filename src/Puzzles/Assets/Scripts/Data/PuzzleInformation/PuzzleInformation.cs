#region

using UnityEngine;

#endregion

namespace Data.PuzzleInformation
{
    public struct PuzzleInformation
    {
        public readonly Texture2D Texture2D;
        public readonly string Id;
        public int ElementsCount;

        public PuzzleInformation(string id, Texture2D texture2D, int elementsCount = 0)
        {
            Texture2D = texture2D;
            ElementsCount = elementsCount;
            Id = id;
        }
    }
}