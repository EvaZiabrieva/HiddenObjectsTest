using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressablesAssetLoader: IAssetLoader
{ 
    public async Task<T> Load<T>(string assetId)
    {
        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(assetId);
        T asset = await handle.Task;
        return asset;
    }

    public async Task<T> Load<T>(AssetReference assetReference)
    {
        AsyncOperationHandle<T> handle = assetReference.LoadAssetAsync<T>();
        T asset = await handle.Task;
        return asset;
    }

    public void Unload<T>(T asset)
    {
        Addressables.Release(asset);
    }
}
