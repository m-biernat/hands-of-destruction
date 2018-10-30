using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 10f;

    [SerializeField]
    private Camera firstPersonCamera;
    [SerializeField]
    private Camera thirdPersonCamera;
    [SerializeField]
    private GameObject cameras;

    private byte defaultCamera = 2;

    void Start()
    {
        SetupCameras();
    }

    public void Toggle()
    {
        firstPersonCamera.enabled = !firstPersonCamera.enabled;
        thirdPersonCamera.enabled = !thirdPersonCamera.enabled;
    }

    private void SetupCameras()
    {
        firstPersonCamera.enabled = defaultCamera == 1 ? true : false;
        thirdPersonCamera.enabled = defaultCamera == 2 ? true : false;
    }

    public Vector3 CalculateRotationY(float yAxisRotation)
    {
        return new Vector3(0f, yAxisRotation, 0f) * mouseSensitivity;
    }

    public Vector3 CalculateRotationX(float xAxisRotation)
    {
        return new Vector3(xAxisRotation, 0f, 0f) * mouseSensitivity;
    }

    public void Rotate(Vector3 rotation)
    {
        // Debug.Log(cameras.transform.eulerAngles.x);
        cameras.transform.Rotate(-rotation);
    }

    public void ChangeDistance(float zAxisDistance)
    {
        float changeFov = thirdPersonCamera.fieldOfView;
        changeFov += (-zAxisDistance * 50);
        changeFov = Mathf.Clamp(changeFov, 60, 90);
        thirdPersonCamera.fieldOfView = changeFov;
    }
}
