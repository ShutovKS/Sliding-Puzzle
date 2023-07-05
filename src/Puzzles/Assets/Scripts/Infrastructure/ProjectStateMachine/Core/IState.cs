namespace Infrastructure.ProjectStateMachine.Core
{
    public interface IState<out TInitializer>
    {
        public TInitializer Initializer { get; }
    }
}