using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameLoopStateMachine : MonoBehaviour
{
    private List<BaseState> _states;
    private BaseState _initialState;
    private BaseState _currentState;

    [Inject]
    public void Construct(LoadLevelState loadLevelState, GameplayState gameplayState, PopupState popupState)
    {
        _initialState = loadLevelState;
        _states = new List<BaseState> { loadLevelState, gameplayState, popupState };
    }

    public void NextState()
    {
        int stateIndex = _currentState == null ? 0 : _states.IndexOf(_currentState) + 1;

        if (stateIndex >= _states.Count)
        {
            stateIndex = 0;
        }

        _currentState = _states[stateIndex];
        _currentState.Start();
    }

    private void Start()
    {
        _currentState = _initialState;
        _currentState.Start();
    }

    private void Update()
    {
        if (_currentState.IsFinished)
            NextState();
    }

    private void OnDestroy()
    {
        _currentState?.Stop();
    }
}
