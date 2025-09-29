using UnityEngine;

public class BuildingsGrid
{
    private readonly int _width;
    private readonly int _height;
    private readonly BuildingEntity[,] grid;

    public BuildingsGrid(int width, int height)
    {
        _width = width;
        _height = height;

        grid = new BuildingEntity[width, height];
    }

    public bool CanPlace(BuildingEntity building, Vector2Int pos)
    {
        if (pos.x < 0 || pos.y < 0 || pos.x > _width - building.Size.x || pos.y > _height - building.Size.y) 
            return false;
        
        for (int x = 0; x < building.Size.x; x++)
        {
            for (int y = 0; y < building.Size.y; y++)
            {
                if (grid[pos.x + x, pos.y + y] != null) 
                    return false;
            }
        }

        return true;
    }

    public void Place(BuildingEntity building, Vector2Int pos)
    {
        for (int x = 0; x < building.Size.x; x++)
        {
            for (int y = 0; y < building.Size.y; y++)
            {
                grid[pos.x + x, pos.y + y] = building;
            }
        }

        building.SetPosition(pos);
    }

    public void Remove(BuildingEntity building)
    {
        for (int x = 0; x < building.Size.x; x++)
        {
            for (int y = 0; y < building.Size.y; y++)
            {
                var pos = building.Position;
                if (grid[pos.x + x, pos.y + y] == building)
                    grid[pos.x + x, pos.y + y] = null;
            }
        }
    }
}