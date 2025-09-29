public class CameraPresenter
{
    private readonly MoveCameraUseCase _moveUseCase;
    private readonly ZoomCameraUseCase _zoomUseCase;
    private readonly CameraState _state;
    private readonly ICameraService _cameraService;
    private readonly ICameraInput _input;

    public CameraPresenter(MoveCameraUseCase moveUseCase, ZoomCameraUseCase zoomUseCase, CameraState state, ICameraService cameraService, ICameraInput input)
    {
        _moveUseCase = moveUseCase;
        _zoomUseCase = zoomUseCase;
        _state = state;
        _cameraService = cameraService;
        _input = input;
    }

    public void Tick()
    {
        _moveUseCase.Execute(_input.ReadMovementDelta());
        _cameraService.SetPosition(_state.Position);
    }
    
    public void LateTick()
    {
        _zoomUseCase.Execute(_input.ReadZoomDelta());
        _cameraService.SetZoom(_state.ZoomLevel);
    }
}