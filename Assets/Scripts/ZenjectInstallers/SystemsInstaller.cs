using UnityEngine;
using Zenject;

public class SystemsInstaller: MonoInstaller
{
    [SerializeField] private Transform _parent;
    [SerializeField] private PopupSystem _popupSystemPrefab;
    public override void InstallBindings()
    {
        Container.Bind<LevelLoaderSystem>().AsSingle();

        Container.Bind<PopupSystem>()
            .FromComponentInNewPrefab(_popupSystemPrefab)
            .UnderTransform(_parent)
            .AsSingle();
    }
}
