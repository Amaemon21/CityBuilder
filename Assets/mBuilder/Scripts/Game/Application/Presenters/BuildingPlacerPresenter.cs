using UnityEngine;

public class BuildingPlacerPresenter : IBuildingPlacerPresenter
{
    private readonly PlaceBuildingUseCase _placeBuilding;
    private readonly CanPlaceBuildingUseCase _canPlaceBuilding;
    private readonly RotateBuildingUseCase _rotateBuilding;

    private BuildingEntity _entity;
    private IBuildingView _view;

    public BuildingPlacerPresenter(PlaceBuildingUseCase placeBuilding, CanPlaceBuildingUseCase canPlaceBuilding, RotateBuildingUseCase rotateBuilding)
    {
        _placeBuilding = placeBuilding;
        _canPlaceBuilding = canPlaceBuilding;
        _rotateBuilding = rotateBuilding;
    }

    public void StartPlacing(BuildingEntity entity, IBuildingView view)
    {
        _entity = entity;
        _view = view;
    }

    public void UpdatePosition(Vector2Int gridPos, Vector3 worldPos)
    {
        if (_entity == null || _view == null) 
            return;

        bool available = _canPlaceBuilding.Execute(_entity, gridPos);

        _view.ShowAt(worldPos);
        _view.SetTransparent(available);
    }
    
    public void UpdateRotation()
    {
        if (_entity == null || _view == null) 
            return;

        _rotateBuilding.Execute(_view);
    }

    public void ConfirmPlacement(Vector2Int gridPos)
    {
        if (_entity == null || _view == null) 
            return;

        if (_placeBuilding.Execute(_entity, gridPos))
        {
            _view.MarkAsPlaced();
            _view.SetNormal();
            Cancel();
        }
    }

    public void Cancel()
    {
        _entity = null;
        _view = null;
    }
}