using UnityEngine;

namespace Services.Factories.UIFactory
{
    public interface IUIInfo
    {
        GameObject LoadingScreen { get; }
        GameObject MainMenuScreen { get; }
        GameObject InGameMenuScreen { get; }
    }
}