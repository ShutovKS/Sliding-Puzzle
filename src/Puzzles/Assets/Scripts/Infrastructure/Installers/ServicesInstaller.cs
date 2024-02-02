#region

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
            BindLoadPuzzlesCatalogData();
        }

        private void BindUIFactory()
        {
            Container.BindInterfacesTo<UIFactory>().AsSingle().NonLazy();
        }

        private void BindLoadPuzzlesCatalogData()
        {
            Container.BindInterfacesTo<LoadPuzzlesCatalogData>().AsSingle().NonLazy();
        }
    }
}