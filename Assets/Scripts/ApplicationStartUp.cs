using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class ApplicationStartUp : MonoBehaviour
{
    [SerializeField] AssetReference _sceneReference;

    private async void Start()
    {
        AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(_sceneReference);
        SceneInstance scene = await handle.Task;
    }
}
