using System.Collections.Generic;
using Zenject;

public class LoadLevelState : BaseState, IInitializable
{
    private const string ALL_LEVELS_CONFIG_ID = "AllLevelsConfig";
    public override bool IsFinished => _isFinished;

    private bool _isFinished = false;

    private LevelLoaderSystem _levelLoader;
    private IConfigSystem _configSystem;

    private List<string> _levelsIds;
    private int _currentLevel;

    public LoadLevelState(LevelLoaderSystem levelLoader, IConfigSystem configSystem)
    {
        _levelLoader = levelLoader;
        _configSystem = configSystem;
        _currentLevel = -1;
    }

    public void Initialize()
    {
        AllLevelsConfig allLevelsConfig = _configSystem.GetConfig<AllLevelsConfig>(ALL_LEVELS_CONFIG_ID);
        _levelsIds = new List<string>(allLevelsConfig.levelsIds);
    }

    public override void Start()
    {
        _isFinished = false;
        _currentLevel++;

        if (_currentLevel >= _levelsIds.Count)
            _currentLevel = 0;

        _levelLoader.LoadLevel(_levelsIds[_currentLevel]);

        Stop();
    }

    public override void Stop()
    {
        _isFinished = true;
    }
}
