using UnityEngine;

namespace Data.PuzzleInformation
{
    [CreateAssetMenu(fileName = "Puzzle information", menuName = "Data/Puzzle Information", order = 0)]
    public class PuzzleInformation : ScriptableObject
    {
        [field: SerializeField] public Texture2D Image { get; private set; }
        [field: SerializeField] public int ElementsCount { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
    }
}