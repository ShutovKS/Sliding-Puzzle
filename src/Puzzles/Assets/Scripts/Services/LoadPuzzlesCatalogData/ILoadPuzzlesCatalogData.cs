using System.Collections.Generic;
using System.Threading.Tasks;
using Data.PuzzleInformation;

namespace Services.LoadPuzzlesCatalogData
{
    public interface ILoadPuzzlesCatalogData
    {
        void LoadData();
        List<PuzzleInformation> GetPuzzlesInformation();
    }
}