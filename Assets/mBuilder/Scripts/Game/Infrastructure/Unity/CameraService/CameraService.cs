using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraService : MonoBehaviour, ICameraService
{
    [field: SerializeField] public Camera Camera { get; private set; }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetZoom(float zoom)
    {
        Camera.orthographicSize = zoom;
    }
}
