using UnityEngine;

[CreateAssetMenu(fileName = "GridConfig", menuName = "Configs/Builder/GridConfig")]
public class GridConfig : ScriptableObject
{
    [field: SerializeField] public string OwnerId { get; private set; }
    [field: SerializeField] public Vector2Int GridSize { get; private set; } = new(10, 10);
}
