using System.Collections.Generic;
using UniRx;
using UnityEngine;
public class LevelLoaderSystem
{
    public readonly ReactiveProperty<string> CurrentLevel = new();

    private ConfigsSystem _configSystem;
    private ItemsModel _itemsModel;
    private ItemViewModel.Factory _viewModelFactory;
    private IItemsPlacerStrategy _itemsPlacer;

    private ObjectPool<ItemView> _itemsViewPool;
    private ObjectPool<InteractableItem> _interactablesPool;

    private List<ItemView> _acitveItemViews = new();

    public LevelLoaderSystem(ConfigsSystem configsSystem, ItemsModel itemsModel, 
                             ItemView.Factory itemsViewFactory, ItemViewModel.Factory viewModelFactory,
                             InteractableItem.Factory interactionControllerFactory,
                             IItemsPlacerStrategy itemsPlacer)
    {
        _configSystem = configsSystem;
        _itemsModel = itemsModel;
        _viewModelFactory = viewModelFactory;
        _itemsPlacer = itemsPlacer;

        _interactablesPool = new ObjectPool<InteractableItem>(interactionControllerFactory.Create);
        _itemsViewPool = new ObjectPool<ItemView>(itemsViewFactory.Create);
    }

    public void LoadLevel(string id)
    {
        UnloadPreviousLevel();

        LevelConfig config = _configSystem.GetConfig<LevelConfig>(id);
        List<RectTransform> interactables = new List<RectTransform>();

        foreach (LevelConfig.ItemInfo itemInfo in config.items)
        {
            _itemsModel.FoundedItems.Add(itemInfo.itemId, new ReactiveProperty<int>());
            _itemsModel.ItemsCount.Add(itemInfo.itemId, itemInfo.count);

            ItemViewModel itemViewModel = _viewModelFactory.Create(itemInfo.itemId);

            ItemView itemView = _itemsViewPool.GetFromPool();
            itemView.Initialize(itemViewModel);
            _acitveItemViews.Add(itemView);

            for (int i = 0; i < itemInfo.count; i++)
            {
                InteractableItem item = _interactablesPool.GetFromPool();
                item.Initialize(itemInfo.itemId, itemViewModel);
                interactables.Add(item.GetComponent<RectTransform>());
            }
        }

        RectTransform holder = interactables[0].parent.GetComponent<RectTransform>();
        _itemsPlacer.PlaceItems(interactables, holder);

        CurrentLevel.Value = id;
    }

    private void UnloadPreviousLevel()
    {
        _itemsModel.Dispose();

        for (int i = 0; i < _acitveItemViews.Count; i++)
        {
            _acitveItemViews[i].gameObject.SetActive(false);
        }
    }
}
