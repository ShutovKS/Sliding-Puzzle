#region

using Services.AssetsAddressablesProvider;
using Services.Factories.AbstractFactory;
using Services.Factories.UIFactory;
using Services.LoadPuzzlesCatalogData;
using Zenject;

#endregion

namespace Infrastructure.Installers
{
    public class ServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindUIFactory();
            BindAbstractFactory();
            BindAssetsAddressablesProvider();
            BindLoadPuzzlesCatalogData();
        }

        private void BindUIFactory()
        {
            Container.BindInterfacesTo<UIFactory>().AsSingle().NonLazy();
        }

        private void BindAbstractFactory()
        {
            Container.BindInterfacesTo<AbstractFactory>().AsSingle().NonLazy();
        }

        private void BindAssetsAddressablesProvider()
        {
            Container.BindInterfacesTo<AssetsAddressablesProvider>().AsSingle().NonLazy();
        }
        
        private void BindLoadPuzzlesCatalogData()
        {
            Container.BindInterfacesTo<LoadPuzzlesCatalogData>().AsSingle().NonLazy();
        }
    }
}