using UnityEngine;

public class CameraFunctions : MonoBehaviour
{
    public static CameraFunctions _cameraFunctions;
    public GameObject _target;
    public float _cameraMaxOffset;
    public float _cameraSpeed;

    private Vector3 _mousePosition;
    private Vector3 _targetPosition;
    private Vector3 _velocityReference;
    private Vector3 _offset;
    private Vector3 _oldOffset;
    private float _zStart;


    private void Awake()
    {
        if (_cameraFunctions == null)
        {
            _cameraFunctions = this;
            _zStart = transform.position.z;
        }
        else if (_cameraFunctions != this)
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        _mousePosition = GetMousePosition();
        _targetPosition = GetTargetPosition();

        UpdateCameraPosition();
    }
    

    private Vector3 GetMousePosition()
    {
        Vector2 position = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        position *= 2;
        position -= Vector2.one;
        float max = 1f;

        if (Mathf.Abs(position.x) > max)
        {
            position.x = position.x < 0 ? -1 : 1;
        }

        if (Mathf.Abs(position.y) > max)
        {
            position.y = position.y < 0 ? -1 : 1;
        }

        return position;
    }

    private Vector3 GetTargetPosition()
    {
        // new offset should change based on the camera position and old offset
        _oldOffset = _offset;
        _offset = Vector3.Lerp(_oldOffset, _mousePosition * _cameraMaxOffset, Time.deltaTime * _cameraSpeed);

        Vector3 newCameraPosition = _target.transform.position + _offset;
        newCameraPosition.z = _zStart;

        return newCameraPosition;
    }

    private void UpdateCameraPosition()
    {
        transform.position = _targetPosition;
    }
}
