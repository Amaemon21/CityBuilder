using UnityEngine;

public class ZoomCameraUseCase
{
    private readonly CameraState _cameraState;
    private readonly ZoomProperties _properties;
    
    private float _orthoSize;
    private float _velocity;

    public ZoomCameraUseCase(CameraState cameraState, ZoomProperties properties)
    {
        _cameraState = cameraState;
        _properties = properties;
        
        _orthoSize = cameraState.ZoomLevel;
    }

    public void Execute(float inputDelta)
    {
        float inputDeltaWithSpeed = inputDelta * _properties.ZoomSpeed;

        _orthoSize = Mathf.Clamp(_orthoSize - inputDeltaWithSpeed, _properties.ZoomMin, _properties.ZoomMax);

        float newOrthoSize = Mathf.SmoothDamp(_cameraState.ZoomLevel, _orthoSize, ref _velocity, _properties.Smoothness);

        _cameraState.ZoomLevel = newOrthoSize;
    }
}