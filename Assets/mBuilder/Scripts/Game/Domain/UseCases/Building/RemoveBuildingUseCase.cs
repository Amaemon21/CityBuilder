public class RemoveBuildingUseCase
{
    private readonly BuildingsGrid _grid;

    public RemoveBuildingUseCase(BuildingsGrid grid)
    {
        _grid = grid;
    }

    public void Execute(BuildingEntity entity)
    {
        _grid.Remove(entity);
    }
}