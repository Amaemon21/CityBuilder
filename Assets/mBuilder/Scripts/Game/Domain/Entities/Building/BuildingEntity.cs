using UnityEngine;

public class BuildingEntity
{
    public Vector2Int Size { get; private set; }
    public Vector2Int Position { get; private set; }

    private Vector2Int _originalSize;
    
    public BuildingEntity(Vector2Int size)
    {
        Size = size;
        _originalSize = size;
    }

    public void SetPosition(Vector2Int pos)
    {
        Position = pos;
    }
    
    public void Rotate()
    {
        Size = new Vector2Int(_originalSize.y, _originalSize.x);
    }
}