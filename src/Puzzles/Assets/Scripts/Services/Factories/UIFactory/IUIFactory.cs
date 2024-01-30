#region

using System.Threading.Tasks;
using UnityEngine;

#endregion

namespace Services.Factories.UIFactory
{
    public interface IUIFactory : IUIInfo
    {
        Task<T> Created<T>() where T : Object;
        void Destroy<T>() where T : Object;
    }
}