using UnityEngine;

public class PlaceBuildingUseCase
{
    private readonly BuildingsGrid _grid;

    public PlaceBuildingUseCase(BuildingsGrid grid)
    {
        _grid = grid;
    }

    public bool Execute(BuildingEntity entity, Vector2Int position)
    {
        if (!_grid.CanPlace(entity, position)) 
            return false;

        _grid.Place(entity, position);
        return true;
    }
}