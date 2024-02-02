#region

#nullable enable
using Object = UnityEngine.Object;

#endregion

namespace Services.Factories.UIFactory
{
    public interface IUIInfo
    {
        T? GetUI<T>() where T : Object;
    }
}