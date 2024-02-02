#region

using System.Collections.Generic;
using Data.PuzzleInformation;

#endregion

namespace Services.LoadPuzzlesCatalogData
{
    public interface ILoadPuzzlesCatalogData
    {
        void LoadData();
        List<PuzzleInformation> GetPuzzlesInformation();
    }
}