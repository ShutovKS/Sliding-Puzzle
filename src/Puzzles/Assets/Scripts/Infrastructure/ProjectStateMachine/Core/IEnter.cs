using System.Threading.Tasks;

namespace Infrastructure.ProjectStateMachine.Core
{
    public interface IEnter
    {
        public void OnEnter();
    }

    public interface IEnter<in T0>
    {
        public void OnEnter(T0 arg0);
    }
}