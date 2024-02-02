#region

using Infrastructure.ProjectStateMachine.Core;
using Infrastructure.ProjectStateMachine.States;
using Services.Factories.UIFactory;
using Services.LoadPuzzlesCatalogData;
using UnityEngine;

#endregion

namespace Infrastructure.ProjectStateMachine
{
    public class Bootstrap : MonoBehaviour
    {
        public void Start()
        {
            IUIFactory uiFactory = new UIFactory();
            ILoadPuzzlesCatalogData loadPuzzlesCatalogData = new LoadPuzzlesCatalogData();

            StateMachine = new StateMachine<Bootstrap>(
                new BootstrapState(this),
                new MainMenuState(this, uiFactory),
                new InGameMenuState(this, uiFactory, loadPuzzlesCatalogData),
                new FoldingThePuzzleState(this, uiFactory)
            );

            StateMachine.SwitchState<BootstrapState>();
        }

        public StateMachine<Bootstrap> StateMachine { get; private set; }
    }
}