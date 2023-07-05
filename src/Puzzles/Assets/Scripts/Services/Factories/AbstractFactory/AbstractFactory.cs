using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Services.Factories.AbstractFactory
{
    public class AbstractFactory : IAbstractFactory
    {
        public Task<T> CreateInstance<T>(string path) where T : Object
        {
            throw new System.NotImplementedException();
        }

        public Task<T> CreateInstance<T>(AssetReference path) where T : Object
        {
            throw new System.NotImplementedException();
        }
    }
}