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
        
        private void BindLoadPuzzlesCatalogData()
        {
            Container.BindInterfacesTo<LoadPuzzlesCatalogData>().AsSingle();
        }
    }
}