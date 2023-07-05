using Infrastructure.ProjectStateMachine.Core;
using Zenject;

namespace Infrastructure.ProjectStateMachine.States
{
    public class BootstrapState : IState<Bootstrap>, IInitializable
    {
        public BootstrapState(Bootstrap initializer)
        {
            Initializer = initializer;
        }
        
        public Bootstrap Initializer { get; }

        public void Initialize()
        {
            Initializer.StateMachine.SwitchState<MainMenuState>();
        }
    }
}