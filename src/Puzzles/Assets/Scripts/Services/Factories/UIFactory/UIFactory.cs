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
        public GameObject InGameMenuScreen { get; private set; }

        public async Task<GameObject> CreatedLoadingScreen()
        {
            var prefab = await _assetsAddressableService.GetAsset<GameObject>(
                AssetsAddressablesConstants.LOADING_SCREEN);

            LoadingScreen = _container.InstantiatePrefab(prefab);
            return LoadingScreen;
        }

        public async Task<GameObject> CreatedInGameMenuScreen()
        {
            var prefab = await _assetsAddressableService.GetAsset<GameObject>(
                AssetsAddressablesConstants.IN_GAME_MENU_SCREEN);

            InGameMenuScreen = _container.InstantiatePrefab(prefab);
            return InGameMenuScreen;
        }

        public async Task<GameObject> CreatedMainMenuScreen()
        {
            var prefab = await _assetsAddressableService.GetAsset<GameObject>(
                AssetsAddressablesConstants.MAIN_MENU_SCREEN);

            MainMenuScreen = _container.InstantiatePrefab(prefab);
            return MainMenuScreen;
        }

        public void DestroyLoadingScreen() => Object.Destroy(LoadingScreen);
        public void DestroyMainMenuScreen() => Object.Destroy(MainMenuScreen);
        public void DestroyInGameMenuScreen() => Object.Destroy(InGameMenuScreen);
    }
}