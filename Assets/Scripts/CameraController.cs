using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    public InputSystem_Actions _input;

    [Header("------CAMERA------")]
    public Camera _camera;
    public float CameraDistance;
    public float CameraRadius;
    public float CameraFollowSpeed;
    public LayerMask LayersDetect;
    public Vector3 CameraOffset;

    [Header("------MOUSE CONTROLL------")]
    public float MousePosY;
    public float MousePosX;
    public float SensitivityY;
    public float SensitivityX;
    public float MinAngleY;
    public float MaxAngleY;


    void Start()
    {
        // ENABLE INPUT
        _input = new InputSystem_Actions();
        _input.Enable();
        // GET MAIN CAMERA
        if (_camera != null) {
            _camera = Camera.main;
        }
    }

    private void Update()
    {

        Debug.DrawRay(transform.position, -transform.forward * CameraDistance, Color.red);

        RaycastHit hit;
        Physics.SphereCast(transform.position, CameraRadius, -transform.forward, out hit, LayersDetect);
        if(Physics.SphereCast(transform.position, CameraRadius, -transform.forward, out hit, LayersDetect))
        {
            Debug.DrawRay(transform.position, -transform.forward * CameraDistance, Color.green);

        }
        else
        {
            Vector3.Lerp(_camera.transform.position, new Vector3(CameraOffset.x, CameraOffset.y, -CameraDistance), CameraFollowSpeed);    
        }

        // READ MOUSE POSITION
        MousePosY += -Mouse.current.delta.ReadValue().y * SensitivityY;
        MousePosX += Mouse.current.delta.ReadValue().x * SensitivityX;

    }


}
