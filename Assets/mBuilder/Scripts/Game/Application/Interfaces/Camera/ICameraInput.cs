using UnityEngine;

public interface ICameraInput
{
    Vector3 ReadMovementDelta();
    float ReadZoomDelta();
}