using Infrastructure.ProjectStateMachine.Core;
using Infrastructure.ProjectStateMachine.States;
using JetBrains.Annotations;
using Services.AssetsAddressablesProvider;
using Services.Factories.AbstractFactory;
using Services.Factories.UIFactory;

namespace Infrastructure.ProjectStateMachine
{
    [UsedImplicitly]
    public class Bootstrap
    {
        public Bootstrap(
            IUIFactory uiFactory,
            IAbstractFactory abstractFactory,
            IAssetsAddressablesProvider assetsAddressablesProvider)
        {
            StateMachine = new StateMachine<Bootstrap>(
                new BootstrapState(this)
            );
        }

        public readonly StateMachine<Bootstrap> StateMachine;
    }
}