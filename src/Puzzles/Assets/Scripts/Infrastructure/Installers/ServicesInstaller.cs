using Services.AssetsAddressablesProvider;
using Services.Factories.AbstractFactory;
using Services.Factories.UIFactory;
using Zenject;

namespace Infrastructure.Installers
{
    public class ServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindUIFactory();
            BindAbstractFactory();
            BindAssetsAddressablesProvider();
        }

        private void BindUIFactory()
        {
            Container.BindInterfacesTo<UIFactory>().AsSingle();
        }

        private void BindAbstractFactory()
        {
            Container.BindInterfacesTo<AbstractFactory>().AsSingle();
        }

        private void BindAssetsAddressablesProvider()
        {
            Container.BindInterfacesTo<AssetsAddressablesProvider>().AsSingle();
        }
    }
}