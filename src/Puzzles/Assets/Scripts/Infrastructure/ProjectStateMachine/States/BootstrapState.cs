#region

using Infrastructure.ProjectStateMachine.Core;

#endregion

namespace Infrastructure.ProjectStateMachine.States
{
    public class BootstrapState : IState<Bootstrap>, IEnter
    {
        public BootstrapState(Bootstrap initializer)
        {
            Initializer = initializer;
        }

        public Bootstrap Initializer { get; }

        public void OnEnter()
        {
            Initializer.StateMachine.SwitchState<MainMenuState>();
        }
    }
}