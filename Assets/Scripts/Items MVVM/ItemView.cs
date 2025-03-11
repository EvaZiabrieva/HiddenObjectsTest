using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ItemView : MonoBehaviour, IDisposable
{
    public class Factory : PlaceholderFactory<ItemView> { }

    [SerializeField] private Text _text;
    [SerializeField] private Image _image;

    private ItemViewModel _viewModel;
    private CompositeDisposable _compositeDisposable = new();
    
    public void Initialize(ItemViewModel viewModel)
    {
        _compositeDisposable?.Dispose();
        _compositeDisposable = new CompositeDisposable();

        _viewModel = viewModel;

        _compositeDisposable.Add(_viewModel.Sprite.Subscribe(new ImageObserver(_image)));
        _compositeDisposable.Add(_viewModel.CountText.Subscribe(new TextObserver(_text)));
    }

    public void Dispose()
    {
        _compositeDisposable?.Dispose();
    }
}