using System.Collections.Generic;

namespace Services.LoadPuzzlesCatalogData
{
    [System.Serializable]
    public class PuzzlesCatalogData
    {
        public List<Category> categories;
    }

    [System.Serializable]
    public class Category
    {
        public string id;
        public string image_path;
        public string name;
        public List<Puzzle> puzzles;
    }

    [System.Serializable]
    public class Puzzle
    {
        public string id;
        public string image_path;
        public int element_count;
        public string name;
    }
}