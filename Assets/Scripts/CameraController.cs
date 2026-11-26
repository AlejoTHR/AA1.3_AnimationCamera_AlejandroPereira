using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    public InputSystem_Actions _input;


    [Header("------MOUSE CONTROLL------")]
    public float MousePosY;
    public float MousePosX;
    public float SensitivityY;
    public float SensitivityX;
    public float MinAngleY;
    public float MaxAngleY;

    public GameObject _player;

    void Start()
    {
        _input = new InputSystem_Actions();
        _input.Enable();

    }

    private void Update()
    {
        // READ MOUSE POSITION
        MousePosY += -Mouse.current.delta.ReadValue().y * SensitivityY;
        MousePosX += Mouse.current.delta.ReadValue().x * SensitivityX;



    }
    private void LateUpdate()
    {

        // ROTATE
        _player.transform.eulerAngles += new Vector3(0, MousePosX, 0); // ROTATION IN X

        // CLAMPS MOUSE Y VALUES BETWEEN MAX AND MIN
        MousePosY = Mathf.Clamp(MousePosY, MinAngleY, MaxAngleY);
        transform.localEulerAngles = new Vector3(MousePosY, 0, 0);



    }


}
