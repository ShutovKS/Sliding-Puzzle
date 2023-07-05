using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Services.AssetsAddressablesProvider
{
    public interface IAssetsAddressablesProvider
    {
        void Initialize();
        Task<T> GetAsset<T>(string address) where T : Object;
        Task<T> GetAsset<T>(AssetReference assetReference) where T : Object;
        void CleanUp();
    }
}