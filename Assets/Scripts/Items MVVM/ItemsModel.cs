using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ItemsModel : IDisposable
{
    public readonly Dictionary<string, ReactiveProperty<int>> FoundedItems = new();
    public Dictionary<string, int> ItemsCount { get; private set; } = new();

    public event Action<string, int> OnItemFound;
    public event Action OnItemsInGroupFound;

    public void ItemFound(string id)
    {
        if(!FoundedItems.ContainsKey(id))
        {
            Debug.LogError($"Item {id} is not presented in level config!");
            return;
        }

        FoundedItems[id].Value++;

        if (FoundedItems[id].Value == ItemsCount[id])
        {
            OnItemsInGroupFound?.Invoke();
        }

        OnItemFound?.Invoke(id, FoundedItems[id].Value);
    }

    public void Dispose()
    {
        FoundedItems.Clear();
        ItemsCount.Clear();
    }
}
