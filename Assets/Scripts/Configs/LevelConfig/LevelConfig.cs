using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelConfig: BaseConfig, ISerializationCallbackReceiver
{
    [Serializable]
    public class ItemInfo
    {
        public string itemId;
        public int count;
    }

    public List<ItemInfo> items;
    public Dictionary<string, int> itemsOnLevel;
    public string environmentSpriteId;

    public void OnBeforeSerialize()
    {
        FillDictionary();
    }

    public void OnAfterDeserialize()
    {
        FillDictionary();
    }

    private void FillDictionary()
    {
        if (items == null) 
            return;

        itemsOnLevel = new Dictionary<string, int>(items.Count);

        foreach(ItemInfo item in items)
        {
            if (itemsOnLevel.ContainsKey(item.itemId))
            {
                itemsOnLevel[item.itemId] = item.count;
                continue;
            }

            itemsOnLevel.Add(item.itemId, item.count);
        }
    }
}
