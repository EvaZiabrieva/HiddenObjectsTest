using JetBrains.Annotations;
using UnityEngine;

public class GameplayState : BaseState
{
    public override bool IsFinished => _isFinished;

    private ItemsModel _itemsModel;
    private bool _isFinished = false;

    private int _groupsCount = 0;
    private int _foundGroupsCount = 0;

    public GameplayState(ItemsModel model)
    {
        _itemsModel = model;
    }

    public override void Start() 
    {
        _isFinished = false;
        _groupsCount = _itemsModel.ItemsCount.Count;
        _itemsModel.OnItemsInGroupFound += ItemsInGroupFound;
    }

    public override void Stop() 
    {
        _itemsModel.OnItemsInGroupFound -= ItemsInGroupFound;
        _foundGroupsCount = 0;
        _isFinished = true;
    }

    private void ItemsInGroupFound()
    {
        _foundGroupsCount++;

        if(_foundGroupsCount == _groupsCount)
            Stop();
    }
}
