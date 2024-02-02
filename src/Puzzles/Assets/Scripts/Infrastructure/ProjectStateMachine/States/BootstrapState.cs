#region

using Infrastructure.ProjectStateMachine.Core;

#endregion

namespace Infrastructure.ProjectStateMachine.States
{
    public class BootstrapState : IState<Bootstrap>
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