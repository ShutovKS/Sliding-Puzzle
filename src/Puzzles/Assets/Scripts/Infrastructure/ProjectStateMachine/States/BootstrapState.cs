#region

using Infrastructure.ProjectStateMachine.Core;
using Zenject;

#endregion

namespace Infrastructure.ProjectStateMachine.States
{
    public class BootstrapState : IState<Bootstrap>, IInitializable
    {
        public BootstrapState(Bootstrap initializer)
        {
            Initializer = initializer;
        }

        public void Initialize()
        {
            Initializer.StateMachine.SwitchState<MainMenuState>();
        }

        public Bootstrap Initializer { get; }
    }
}