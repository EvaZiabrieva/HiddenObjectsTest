using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InteractableItem : MonoBehaviour
{
    public class Factory : PlaceholderFactory<InteractableItem> { }

    [SerializeField] private Image _image;

    [Inject]
    private ItemsModel _model;

    private string _id;
    private IDisposable _handle;

    public void Initialize(string id, ItemViewModel viewModel)
    {
        _id = id;
        _handle = viewModel.Sprite.Subscribe(new ImageObserver(_image));
    }

    public void OnItemFound()
    {
        _model.ItemFound(_id);
    }

    private void OnDestroy()
    {
        _handle?.Dispose();
    }
}
