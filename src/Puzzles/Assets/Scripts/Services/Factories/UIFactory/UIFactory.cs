#region

using System.Threading.Tasks;
using Data.AssetsAddressablesConstants;
using Services.AssetsAddressablesProvider;
using UnityEngine;
using Zenject;

#endregion

namespace Services.Factories.UIFactory
{
    public class UIFactory : IUIFactory
    {
        public UIFactory(DiContainer container, IAssetsAddressablesProvider assetsAddressableService)
        {
            _container = container;
            _assetsAddressableService = assetsAddressableService;
        }

        private readonly IAssetsAddressablesProvider _assetsAddressableService;

        private readonly DiContainer _container;

        public GameObject LoadingScreen { get; private set; }
        public GameObject MainMenuScreen { get; private set; }
        public GameObject InGameMenuScreen { get; private set; }
        public GameObject FoldingThePuzzle { get; private set; }

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

        public async Task<GameObject> CreatedFoldingThePuzzle()
        {
            var prefab = await _assetsAddressableService.GetAsset<GameObject>(
                AssetsAddressablesConstants.FOLDING_THE_PUZZLE_SCREEN);

            FoldingThePuzzle = _container.InstantiatePrefab(prefab);
            return FoldingThePuzzle;
        }

        public void DestroyLoadingScreen()
        {
            Object.Destroy(LoadingScreen);
        }

        public void DestroyMainMenuScreen()
        {
            Object.Destroy(MainMenuScreen);
        }

        public void DestroyInGameMenuScreen()
        {
            Object.Destroy(InGameMenuScreen);
        }

        public void DestroyFoldingThePuzzle()
        {
            Object.Destroy(FoldingThePuzzle);
        }
    }
}