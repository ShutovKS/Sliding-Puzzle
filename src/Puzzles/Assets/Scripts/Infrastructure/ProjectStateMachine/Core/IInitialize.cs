#region

using System.Threading.Tasks;

#endregion

namespace Infrastructure.ProjectStateMachine.Core
{
    public interface IInitialize
    {
        Task OnInitialize();
    }
}