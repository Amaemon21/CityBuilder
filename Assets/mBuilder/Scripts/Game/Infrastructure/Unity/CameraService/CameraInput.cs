using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraInput : MonoBehaviour, ICameraInput
{
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _mouseSensitivity = 1f;
    [SerializeField] private float _keyboardSensitivity = 1f;

    private Camera _camera;
    private bool _dragging;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    public Vector3 ReadMovementDelta()
    {
        Vector3 mouseInputDelta = ReadMouseInputDelta();
        Vector3 keyboardInputDelta = ReadKeyboardInputDelta();

        return (mouseInputDelta + keyboardInputDelta) * _camera.orthographicSize;
    }

    private Vector3 ReadMouseInputDelta()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsClickOnGround())
            {
                _dragging = true;
            }

            return Vector3.zero;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _dragging = false;
            return Vector3.zero;
        }

        if (_dragging && Input.GetMouseButton(0))
        {
            return Input.mousePositionDelta * _mouseSensitivity;
        }

        return Vector3.zero;
    }
    
    private Vector3 ReadKeyboardInputDelta()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        return new Vector3(-horizontal, -vertical, 0) * _keyboardSensitivity;
    }
    
    public float ReadZoomDelta()
    {
        return Input.mouseScrollDelta.y;
    }

    private bool IsClickOnGround()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, _groundMask))
            return true;
        
        return false;
    }
}