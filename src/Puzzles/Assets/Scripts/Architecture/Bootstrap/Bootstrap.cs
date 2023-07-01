#region

using UnityEngine;
using UnityEngine.SceneManagement;

#endregion

namespace Architecture.Bootstrap
{
    [DefaultExecutionOrder(100)]
    public static class Bootstrap
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void OnAfterAssembliesLoadedRuntimeMethod()
        {
            SceneManager.LoadScene("Scenes/MainMenu");
        }
    }
}