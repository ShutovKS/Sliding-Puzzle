#region

using System.Threading.Tasks;
using UnityEngine;

#endregion

namespace Services.Factories.UIFactory
{
    public interface IUIFactory : IUIInfo
    {
        Task<GameObject> CreatedLoadingScreen();
        void DestroyLoadingScreen();

        Task<GameObject> CreatedMainMenuScreen();
        void DestroyMainMenuScreen();

        Task<GameObject> CreatedInGameMenuScreen();
        void DestroyInGameMenuScreen();

        Task<GameObject> CreatedFoldingThePuzzle();
        void DestroyFoldingThePuzzle();
    }
}