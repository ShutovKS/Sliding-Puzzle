#region

using System.Collections.Generic;

#endregion

namespace Data.GameDifficulty
{
    public static class NumberOfItemsPerDifficulties
    {
        public readonly static Dictionary<string, int> GetPartsCountPerMode = new()
        {
            [GameDifficulties.HARDCORE_MODE] = 5,
            [GameDifficulties.NORMAL_MODE] = 4,
            [GameDifficulties.EASY_MODE] = 3
        };
    }
}