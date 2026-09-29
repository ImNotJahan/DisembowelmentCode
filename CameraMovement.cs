using UnityEngine;

[RequireComponent(typeof(Camera))]
public class camMovement : MonoBehaviour
{
    [Header("Mouse Look Settings")]
    // 1 = the point grabbed stays under the cursor. Scales with the camera's FOV, so zooming in slows the drag.
    // Measured in cursor movement rather than raw mouse counts so it feels the same on Mac and Windows.
    [SerializeField] private float dragMultiplier = 1f;

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

    private Vector3 lastMousePosition;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        
        if (cam != null) cam.fieldOfView = normalFov;
    }

    void Update()
    {
        // Look
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 mouseDelta = Input.mousePosition - lastMousePosition;
            lastMousePosition = Input.mousePosition;

            // The vertical FOV spans Screen.height pixels, so this is independent of resolution and Retina scaling
            float degreesPerPixel = cam.fieldOfView / Screen.height * dragMultiplier;

            float mouseX = -mouseDelta.x * degreesPerPixel;
            float mouseY = -mouseDelta.y * degreesPerPixel;

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
