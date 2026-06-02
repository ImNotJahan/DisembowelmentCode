using UnityEngine;

[RequireComponent(typeof(Camera))]
public class camMovement : MonoBehaviour
{
    [Header("Mouse Look Settings")]
    [SerializeField] private float sensitivity = 2f;
    [SerializeField] private float webSensitivityMultiplier = 0.15f;

    [Header("Clamp Values")]
    [SerializeField] private float minPitch = -60f;
    [SerializeField] private float maxPitch = 60f;
    [SerializeField] private float minYaw   = -80f;
    [SerializeField] private float maxYaw   = 80f;

    [Header("Zoom Settings")]
    [SerializeField] private float normalFov  = 60f;
    [SerializeField] private float zoomedFov  = 30f;
    [SerializeField] private float zoomSpeed  = 8f;

    private float pitch = 0f;
    private float yaw   = 0f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        
        if (cam != null) cam.fieldOfView = normalFov;
    }

    void Update()
    {
        // Look
        if (Input.GetMouseButton(0))
        {
            float effectiveSensitivity = sensitivity *
                (Application.platform == RuntimePlatform.WebGLPlayer ? webSensitivityMultiplier : 1f);

            float mouseX = -Input.GetAxis("Mouse X") * effectiveSensitivity;
            float mouseY = -Input.GetAxis("Mouse Y") * effectiveSensitivity;

            yaw   = Mathf.Clamp(yaw   + mouseX, minYaw,   maxYaw  );
            pitch = Mathf.Clamp(pitch - mouseY, minPitch, maxPitch);

            transform.localRotation = Quaternion.Euler(pitch, yaw, 0f);
        }

        // Zoom
        if (cam != null)
        {
            float targetFov = Input.GetMouseButton(1) ? zoomedFov : normalFov;

            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFov, Time.deltaTime * zoomSpeed);
        }
    }
}
