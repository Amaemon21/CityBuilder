using UnityEngine;

public class BuildingService : MonoBehaviour
{
    [SerializeField] private GridConfig _gridConfig;
    [SerializeField] private Transform _groundPlane;
    [SerializeField] private Camera _mainCamera;

    private BuildingsGrid _grid;
    private IBuildingPlacerPresenter _presenter;
    
    private BuildingView _flyingView;
    
    private void Awake()
    {
        _grid = new BuildingsGrid(_gridConfig.GridSize.x, _gridConfig.GridSize.y);
        
        PlaceBuildingUseCase placeBuilding = new PlaceBuildingUseCase(_grid);
        RotateBuildingUseCase rotateBuildingUse = new RotateBuildingUseCase(_grid);
        CanPlaceBuildingUseCase canPlaceBuilding = new CanPlaceBuildingUseCase(_grid);
        
        _presenter = new BuildingPlacerPresenter(placeBuilding, canPlaceBuilding, rotateBuildingUse);
    }

    public void StartPlacingBuilding(BuildingView prefab)
    { 
        if (_flyingView != null && !_flyingView.IsPlaced)
            Destroy(_flyingView.gameObject);
        
        _flyingView = Instantiate(prefab);
        BuildingEntity entity = new BuildingEntity(_flyingView.Size);
        
        _flyingView.Init(entity);
        
        _presenter.StartPlacing(entity, _flyingView);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (TryGetGridPosition(out Vector2Int gridPos, out Vector3 snappedWorldPos))
            {
                _presenter.ConfirmPlacement(gridPos);
            }
        }
        else
        {
            if (TryGetGridPosition(out Vector2Int gridPos, out Vector3 snappedWorldPos))
            {
                _presenter.UpdatePosition(gridPos, snappedWorldPos);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                _presenter.UpdateRotation();
            }
        }
    }

    private bool TryGetGridPosition(out Vector2Int gridPos, out Vector3 worldPos)
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(_groundPlane.up, _groundPlane.position);

        if (groundPlane.Raycast(ray, out float enter))
        {
            Vector3 hitPos = ray.GetPoint(enter);
            Vector3 localPos = _groundPlane.InverseTransformPoint(hitPos);

            int x = Mathf.RoundToInt(localPos.x);
            int y = Mathf.RoundToInt(localPos.z);

            gridPos = new Vector2Int(x, y);
            worldPos = _groundPlane.TransformPoint(new Vector3(x, 0, y));
            return true;
        }

        gridPos = default;
        worldPos = default;
        
        return false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (_groundPlane == null || _gridConfig == null)
            return;
        
        Gizmos.color = Color.gray;
        float offset = 0.5f;
        
        for (int x = 0; x <= _gridConfig.GridSize.x; x++)
        {
            float xPos = x - offset;
            Vector3 start = _groundPlane.TransformPoint(new Vector3(xPos, 0, -offset));
            Vector3 end = _groundPlane.TransformPoint(new Vector3(xPos, 0, _gridConfig.GridSize.y - offset));
            Gizmos.DrawLine(start, end);
        }

        for (int y = 0; y <= _gridConfig.GridSize.y; y++)
        {
            float yPos = y - offset;
            Vector3 start = _groundPlane.TransformPoint(new Vector3(-offset, 0, yPos));
            Vector3 end = _groundPlane.TransformPoint(new Vector3(_gridConfig.GridSize.x - offset, 0, yPos));
            Gizmos.DrawLine(start, end);
        }
    } 
#endif
}