using UnityEngine;
using Zenject;

public class ItemsInstaller : MonoInstaller
{
    [SerializeField] private ItemView _itemViewPrefab;
    [SerializeField] private InteractableItem _interactabletemPrefab;
    [SerializeField] private Transform _interactItemsHolder;
    [SerializeField] private Transform _itemsHolder;

    public override void InstallBindings()
    {
        Container.Bind<ItemsModel>().AsSingle();
        Container.BindFactory<string, ItemViewModel, ItemViewModel.Factory>().AsSingle();

        Container.BindFactory<ItemView, ItemView.Factory>()
            .FromComponentInNewPrefab(_itemViewPrefab)
            .UnderTransform(_itemsHolder)
            .AsSingle();

        Container.BindFactory<InteractableItem, InteractableItem.Factory>()
            .FromComponentInNewPrefab(_interactabletemPrefab)
            .UnderTransform(_interactItemsHolder)
            .AsSingle();

        Container.BindInterfacesAndSelfTo<RandomItemsPlacer>().AsSingle();
    }
}