using System.Threading.Tasks;
using Data.AssetsAddressablesConstants;
using Services.AssetsAddressablesProvider;
using UnityEngine;
using Zenject;

namespace Services.Factories.UIFactory
{
    public class UIFactory : IUIFactory
    {
        public UIFactory(DiContainer container, IAssetsAddressablesProvider assetsAddressableService)
        {
            _container = container;
            _assetsAddressableService = assetsAddressableService;
        }

        private readonly DiContainer _container;
        private readonly IAssetsAddressablesProvider _assetsAddressableService;

        public GameObject LoadingScreen { get; private set; }
        public GameObject MainMenuScreen { get; private set; }

        public async Task<GameObject> CreatedLoadingScreen()
        {
            var gameObject = await _assetsAddressableService.GetAsset<GameObject>(
                AssetsAddressablesConstants.LOADING_SCREEN);

            LoadingScreen = _container.InstantiatePrefab(gameObject);
            return LoadingScreen;
        }

        public void DestroyLoadingScreen()
        {
            Object.Destroy(LoadingScreen);
        }

        public async Task<GameObject> CreatedMainMenuScreen()
        {
            var gameObject = await _assetsAddressableService.GetAsset<GameObject>(
                AssetsAddressablesConstants.MAIN_MENU_SCREEN);

            MainMenuScreen = _container.InstantiatePrefab(gameObject);
            return MainMenuScreen;
        }

        public void DestroyMainMenuScreen()
        {
            Object.Destroy(MainMenuScreen);
        }
    }
}