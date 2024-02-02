#region

using System;
using System.Collections.Generic;

#endregion

namespace Services.LoadPuzzlesCatalogData
{
    [Serializable]
    public class PuzzlesCatalogData
    {
        public List<Category> categories;
    }

    [Serializable]
    public class Category
    {
        public string id;
        public string image_path;
        public string name;
        public List<Puzzle> puzzles;
    }

    [Serializable]
    public class Puzzle
    {
        public string id;
        public string image_path;
        public int element_count;
        public string name;
    }
}