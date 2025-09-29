using UnityEngine;

public interface IBuildingView
{
    void MarkAsPlaced();
    void ShowAt(Vector3 worldPosition);
    void SetTransparent(bool available);
    void SetNormal();
    BuildingEntity Entity { get; }
}