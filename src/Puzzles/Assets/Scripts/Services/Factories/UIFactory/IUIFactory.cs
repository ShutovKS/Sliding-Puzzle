using System.Threading.Tasks;
using UnityEngine;

namespace Services.Factories.UIFactory
{
    public interface IUIFactory : IUIInfo
    {
        Task<GameObject> CreatedLoadingScreen();
        void DestroyLoadingScreen();
        
        Task<GameObject> CreatedMainMenuScreen();
        void DestroyMainMenuScreen();
    }
}