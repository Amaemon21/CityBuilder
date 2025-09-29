using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CameraService _cameraService;
    [SerializeField] private CameraInput _cameraInput;
    
    [SerializeField] private CameraMovementProperties _movementProperties;
    [SerializeField] private ZoomProperties _zoomProperties;

    private CameraPresenter _presenter;
    private CameraState _state;

    private void Awake()
    {
        _state = new CameraState
        {
            Position = _movementProperties.Pivot.position,
            ZoomLevel = _cameraService.Camera.orthographicSize
        };

        MoveCameraUseCase moveCameraUseCase = new MoveCameraUseCase(_state, _movementProperties);
        ZoomCameraUseCase zoomCameraUseCase = new ZoomCameraUseCase(_state, _zoomProperties);
        
        _presenter = new CameraPresenter(moveCameraUseCase, zoomCameraUseCase, _state, _cameraService, _cameraInput);
    }

    private void Update()
    {
        _presenter.Tick();
    }

    private void LateUpdate()
    {
        _presenter.LateTick();
    }
}