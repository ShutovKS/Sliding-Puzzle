using System.Threading.Tasks;

namespace Infrastructure.ProjectStateMachine.Core
{
    public interface IInitialize
    {
        Task OnInitialize();
    }
}