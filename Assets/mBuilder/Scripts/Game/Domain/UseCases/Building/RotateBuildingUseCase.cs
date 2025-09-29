using UnityEngine;

public class RotateBuildingUseCase
{
    private readonly BuildingsGrid _grid;

    public RotateBuildingUseCase(BuildingsGrid grid)
    {
        _grid = grid;
    }

    public bool Execute(IBuildingView view)
    {
        BuildingView buildingView = view as BuildingView;
        
        if (buildingView == null) 
            return false;
        
        BuildingEntity entity = buildingView.Entity;
        
        entity.Rotate();
        
        buildingView.transform.Rotate(Vector3.up, 90f);
        
        return true;
    }
}