using System.Collections.Generic;
using UnityEngine;

public interface IItemsPlacerStrategy
{
    void PlaceItems(List<RectTransform> items, RectTransform holder);
}
