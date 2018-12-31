using UnityEngine;

// This class is responsible for player cameras and their behaviour.
public class CameraManager : MonoBehaviour
{
    // Those fields contain both cameras and their container (cameras).
    [SerializeField] private Camera firstPersonCamera;
    [SerializeField] private Camera thirdPersonCamera;
    [SerializeField] private GameObject cameras;

    private byte defaultCamera = ClientSettings.defaultCamera;
    private float camRotation = 0f;

    // Changes (toggles) active camera.
    public void Toggle()
    {
        firstPersonCamera.enabled = !firstPersonCamera.enabled;
        thirdPersonCamera.enabled = !thirdPersonCamera.enabled;
    }

    // Sets up a camera based on defaultCamera value.
    public void SetupCameras()
    {
        cameras.SetActive(true);
        firstPersonCamera.enabled = defaultCamera == 1 ? true : false;
        thirdPersonCamera.enabled = defaultCamera == 2 ? true : false;
    }

    // Returns a vector for Y axis rotation. (for horizontal rotation)
    public Vector3 CalculateRotationY(float yAxisRotation)
    {
        return new Vector3(0f, yAxisRotation, 0f) * ClientSettings.mouseSensitivity;
    }

    // Returns a vector for X axis rotation. (for vertical rotation)
    public Vector3 CalculateRotationX(float xAxisRotation)
    {
        camRotation += xAxisRotation * ClientSettings.mouseSensitivity;
        camRotation = Mathf.Clamp(camRotation, -45f, 45f);
        return new Vector3(camRotation, 0f, 0f);
    }

    // Rotates cameras container vertically.
    public void Rotate(Vector3 rotation)
    {
        cameras.transform.localEulerAngles = -rotation;
    }

    // Changes distance from container for third person camera.
    // This changes fov for now, maybe we should change it later.
    public void ChangeDistance(float zAxisDistance)
    {
        float changeFov = thirdPersonCamera.fieldOfView;
        changeFov += (-zAxisDistance * 50);
        changeFov = Mathf.Clamp(changeFov, 60, 90);
        thirdPersonCamera.fieldOfView = changeFov;
    }

    // Return cameras container.
    public GameObject GetCameras() {
        return cameras;
    }
}
