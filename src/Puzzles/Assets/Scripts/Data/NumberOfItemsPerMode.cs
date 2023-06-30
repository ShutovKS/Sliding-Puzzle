using System.Collections.Generic;

namespace Data.PlayerPrefs
{
    public static class NumberOfItemsPerMode
    {
        public readonly static Dictionary<string, int> OfItemsPerMode = new()
        {
            [GameModesTypes.HARDCORE_MODE] = 6,
            [GameModesTypes.NORMAL_MODE] = 4,
            [GameModesTypes.EASY_MODE] = 3
        };
    }
}