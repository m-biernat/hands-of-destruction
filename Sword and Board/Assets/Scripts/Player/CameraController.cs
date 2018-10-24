using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivity = 10f;

    [SerializeField]
    private Camera firstPersonCamera;
    [SerializeField]
    private Camera thirdPersonCamera;

    [SerializeField]
    private float distance = 2f;

    private byte defaultCamera = 1;

    void Start()
    {
        SetupCamera();
    }

    public void Toggle()
    {
        firstPersonCamera.enabled = !firstPersonCamera.enabled;
        thirdPersonCamera.enabled = !thirdPersonCamera.enabled;
    }

    // TODO: Maybe change this in the future.
    private void SetupCamera()
    {
        firstPersonCamera.enabled = defaultCamera == 1 ? false : true;
        thirdPersonCamera.enabled = defaultCamera == 1 ? true : false;
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
        if(firstPersonCamera.enabled)
            firstPersonCamera.transform.Rotate(-rotation);
        else
            thirdPersonCamera.transform.parent.Rotate(-rotation);
    }
}
