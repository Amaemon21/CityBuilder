using UnityEngine;

public class CanPlaceBuildingUseCase
{
    private readonly BuildingsGrid grid;

    public CanPlaceBuildingUseCase(BuildingsGrid grid)
    {
        this.grid = grid;
    }

    public bool Execute(BuildingEntity entity, Vector2Int position)
    {
        return grid.CanPlace(entity, position);
    }
}