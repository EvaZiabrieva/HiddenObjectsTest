using System;
using UniRx;
using UnityEngine;
using Zenject;

public class ItemViewModel : IDisposable, IObserver<int>
{
    public class Factory : PlaceholderFactory<string, ItemViewModel> {}

    public ReactiveProperty<string> CountText { get; } = new();
    public ReactiveProperty<Sprite> Sprite { get; } = new();

    private string _itemId;
    private string _maxCountText;

    private ItemsModel _model;

    private IAssetLoader _assetLoader;
    private IConfigSystem _configSystem;
    private IDisposable _handle;

    public ItemViewModel(string itemId, ItemsModel model,
                         IAssetLoader assetLoader, IConfigSystem configSystem)
    {
        _itemId = itemId;
        _model = model;
        _assetLoader = assetLoader;
        _configSystem = configSystem;
    }

    [Inject]
    public void Initialize()
    {
        if (!_model.FoundedItems.TryGetValue(_itemId, out ReactiveProperty<int> countProperty))
        {
            Debug.LogError($"Item {_itemId} is not presented in model");
            return;
        }

        _maxCountText =  "/" + _model.ItemsCount[_itemId].ToString();
        _handle = countProperty.Subscribe(this);

        OnNext(0);
        LoadSprite();
    }

    private async void LoadSprite()
    {
        string spriteId = _configSystem.GetConfig<ItemConfig>(_itemId).spriteId;
        Sprite sprite = await _assetLoader.Load<Sprite>(spriteId);
        Sprite.Value = sprite;
    }

    public void Dispose()
    {
        _handle?.Dispose();
        _handle = null;
    }

    public void OnNext(int value) 
    {
        CountText.Value = value.ToString() + _maxCountText;
    }

    public void OnCompleted() { }

    public void OnError(Exception error) { }
}
