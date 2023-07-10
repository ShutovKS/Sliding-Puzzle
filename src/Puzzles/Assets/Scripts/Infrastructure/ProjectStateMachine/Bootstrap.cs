#region

using Infrastructure.ProjectStateMachine.Core;
using Infrastructure.ProjectStateMachine.States;
using JetBrains.Annotations;
using Services.AssetsAddressablesProvider;
using Services.Factories.AbstractFactory;
using Services.Factories.UIFactory;
using Services.LoadPuzzlesCatalogData;

#endregion

namespace Infrastructure.ProjectStateMachine
{
    [UsedImplicitly]
    public class Bootstrap
    {
        public Bootstrap(
            IUIFactory uiFactory,
            IAbstractFactory abstractFactory,
            IAssetsAddressablesProvider assetsAddressablesProvide,
            ILoadPuzzlesCatalogData loadPuzzlesCatalogData)
        {
            StateMachine = new StateMachine<Bootstrap>(
                new BootstrapState(this),
                new MainMenuState(this, uiFactory),
                new InGameMenu(this, uiFactory, abstractFactory, loadPuzzlesCatalogData),
                new FoldingThePuzzleState(this, uiFactory)
            );
        }

        public readonly StateMachine<Bootstrap> StateMachine;
    }
}