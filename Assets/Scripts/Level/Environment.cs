using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Environment : MonoBehaviour, IObserver<string>
{
    [SerializeField] private Image _background;

    private LevelLoaderSystem _levelSystem;
    private IConfigSystem _configSystem;
    private IAssetLoader _assetLoader;

    private IDisposable _handle;

    [Inject]
    public void Construct(LevelLoaderSystem levelSystem, IConfigSystem configSystem, IAssetLoader assetLoader)
    {
        _levelSystem = levelSystem;
        _configSystem = configSystem;
        _assetLoader = assetLoader;
    }

    public void Start()
    {
        _handle = _levelSystem.CurrentLevel.Subscribe(this);

        string currentLevel = _levelSystem.CurrentLevel.Value;
        if (!string.IsNullOrEmpty(currentLevel))
        {
            OnNext(currentLevel);
        }
    }

    private void OnDestroy()
    {
        _handle?.Dispose();
    }

    public async void OnNext(string value) 
    {
        LevelConfig config =_configSystem.GetConfig<LevelConfig>(value);
        Sprite sprite = await _assetLoader.Load<Sprite>(config.environmentSpriteId);
        _background.sprite = sprite;
    }

    public void OnCompleted() {}

    public void OnError(Exception error) {}
}
