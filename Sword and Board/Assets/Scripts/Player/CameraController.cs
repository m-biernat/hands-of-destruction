using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 10f;

    [SerializeField] private Camera firstPersonCamera;
    [SerializeField] private Camera thirdPersonCamera;
    [SerializeField] private GameObject cameras;

    private byte defaultCamera = 2;
    private float camRotation = 0f;

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
        camRotation += xAxisRotation * mouseSensitivity;
        camRotation = Mathf.Clamp(camRotation, -45f, 45f);
        return new Vector3(camRotation, 0f, 0f);
    }

    public void Rotate(Vector3 rotation)
    {
        cameras.transform.localEulerAngles = -rotation;
    }

    public void ChangeDistance(float zAxisDistance)
    {
        float changeFov = thirdPersonCamera.fieldOfView;
        changeFov += (-zAxisDistance * 50);
        changeFov = Mathf.Clamp(changeFov, 60, 90);
        thirdPersonCamera.fieldOfView = changeFov;
    }
}
