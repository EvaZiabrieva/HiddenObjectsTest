using System.Collections.Generic;
using UnityEngine;

public class RandomItemsPlacer : IItemsPlacerStrategy
{
    public void PlaceItems(List<RectTransform> items, RectTransform holder)
    {
        List<Vector2> grid = CalculateGridCell(holder.rect, items[0].rect.size);

        for (int i = 0; i < items.Count; i++)
        {
            int cellIndex = Random.Range(0, grid.Count);
            items[i].localPosition = grid[cellIndex];
            grid.RemoveAt(cellIndex);   
        }
    }

    private List<Vector2> CalculateGridCell(Rect rect, Vector2 cellSize)
    {
        int cellsWidth = Mathf.FloorToInt(rect.width / cellSize.x);
        int cellsHeight = Mathf.FloorToInt(rect.height / cellSize.y);

        List<Vector2> grid = new List<Vector2>(cellsWidth * cellsHeight);

        for (int i = 0; i < cellsWidth; i++)
        {
            for (int j = 0; j < cellsHeight; j++)
            {
                float offset_x = Random.Range(-(cellSize.x / 4), (cellSize.x / 4));
                float offset_y = Random.Range(-(cellSize.y / 4), (cellSize.y / 4));

                float x = rect.min.x + (cellSize.x * (i + 1));
                float y = rect.min.y + (cellSize.y * (j + 1));

                grid.Add(new Vector2(x + offset_x, y + offset_y));
            }
        }

        return grid;
    }
}
