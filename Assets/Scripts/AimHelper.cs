using Mono.Cecil;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimHelper : MonoBehaviour
{
    public Transform camTransform;
    [Range(1f, 100f)]
    public float sensitivity = 5f;

    private Vector2 _deltaValue;
    private float _yaw;
    private float _pitch;
   
    void Start()
    {
        Vector3 euler = camTransform.rotation.eulerAngles;
        _pitch = euler.x;
        _yaw = euler.y;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnLook(InputValue value)
    {
        _deltaValue = value.Get<Vector2>();
    }

    
    void Update()
    {
        _pitch -= _deltaValue.y * sensitivity * Time.deltaTime;
        _pitch = Mathf.Clamp(_pitch, -90f, 90f);

        _yaw += _deltaValue.x * sensitivity * Time.deltaTime;
        camTransform.rotation = Quaternion.Euler(_pitch, _yaw, 0f);
    }
}
