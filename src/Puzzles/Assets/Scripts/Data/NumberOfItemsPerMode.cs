using System.Collections.Generic;

namespace Data.PlayerPrefs
{
    public static class NumberOfItemsPerMode
    {
        public readonly static Dictionary<string, int> OfItemsPerMode = new()
        {
            [GameModesTypes.HARDCORE_MODE] = 7,
            [GameModesTypes.NORMAL_MODE] = 5,
            [GameModesTypes.EASY_MODE] = 3
        };
    }
}