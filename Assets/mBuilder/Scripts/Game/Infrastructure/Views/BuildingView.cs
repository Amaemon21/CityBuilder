using UnityEngine;

public class BuildingView : MonoBehaviour, IBuildingView
{
    public Renderer MainRenderer;
    public Vector2Int Size = Vector2Int.one;
    public BuildingEntity Entity { get; private set; }

    public bool IsPlaced { get; private set; }

    public void Init(BuildingEntity entity)
    {
        Entity = entity;
        IsPlaced = false;
    }

    public void MarkAsPlaced()
    {
        IsPlaced = true;
    }

    public void ShowAt(Vector3 worldPosition)
    {
        transform.position = worldPosition;
    }

    public void SetTransparent(bool available)
    {
        MainRenderer.material.color = available ? Color.green : Color.red;
    }

    public void SetNormal()
    {
        MainRenderer.material.color = Color.white;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        for (int x = 0; x < Size.x; x++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                Gizmos.color = ((x + y) % 2 == 0) ? new Color(0.88f, 0f, 1f, 0.3f) : new Color(1f, 0.68f, 0f, 0.3f);
                Vector3 localPos = new Vector3(x, 0, y);
                Vector3 worldPos = transform.TransformPoint(localPos);
                Gizmos.matrix = Matrix4x4.TRS(worldPos, transform.rotation, Vector3.one);
                Gizmos.DrawCube(Vector3.zero, new Vector3(1, 0.1f, 1));
            }
        }

        Gizmos.matrix = Matrix4x4.identity;
    }
#endif
}