#region

#nullable enable
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

#endregion

namespace Services.Factories.UIFactory
{
    public interface IUIInfo
    {
        T? GetUI<T>() where T : Object;
    }
}