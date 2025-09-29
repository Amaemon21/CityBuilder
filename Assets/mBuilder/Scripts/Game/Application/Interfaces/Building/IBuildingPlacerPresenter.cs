using UnityEngine;

public interface IBuildingPlacerPresenter
{
    void StartPlacing(BuildingEntity entity, IBuildingView view);
    void UpdatePosition(Vector2Int gridPos, Vector3 worldPos);
    void UpdateRotation();
    void ConfirmPlacement(Vector2Int gridPos);
    void Cancel();
}