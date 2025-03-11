using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private ConfigsSystem _configsPrefab;
    
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<AddressablesAssetLoader>().AsSingle();

        Container.BindInterfacesAndSelfTo<ConfigsSystem>()
           .FromComponentInNewPrefab(_configsPrefab)
           .AsSingle();
    }
}