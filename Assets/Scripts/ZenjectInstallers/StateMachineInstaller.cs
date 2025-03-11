using UnityEngine;
using Zenject;

public class StateMachineInstaller : MonoInstaller
{
    [SerializeField] private GameLoopStateMachine _stateMachinePrefab;
    public override void InstallBindings()
    {
        Container.Bind<GameLoopStateMachine>()
            .FromComponentInNewPrefab(_stateMachinePrefab)
            .AsSingle()
            .NonLazy();

        Container.Bind<GameplayState>().AsSingle();
        Container.BindInterfacesAndSelfTo<LoadLevelState>().AsSingle();
        Container.Bind<PopupState>().AsSingle();
    }
}