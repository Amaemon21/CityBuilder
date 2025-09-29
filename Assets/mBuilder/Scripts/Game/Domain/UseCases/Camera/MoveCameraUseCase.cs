using UnityEngine;

public class MoveCameraUseCase
{
    private readonly CameraState _cameraState;
    private readonly CameraMovementProperties _properties;
    
    private Vector3 _cachedCameraPosition;

    public MoveCameraUseCase(CameraState cameraState, CameraMovementProperties cameraMovementProperties)
    {
        _cameraState = cameraState;
        _properties = cameraMovementProperties;
        
        _cachedCameraPosition = new Vector3(cameraState.Position.x, cameraMovementProperties.Pivot.position.y, cameraState.Position.z);
    }

    public void Execute(Vector3 inputDelta)
    {
        _cachedCameraPosition -= new Vector3(inputDelta.x, 0, inputDelta.y) * _properties.Speed;

        _cameraState.Position = Vector3.Lerp(_cameraState.Position, _cachedCameraPosition, Time.deltaTime / _properties.Smoothness);    
    }
}