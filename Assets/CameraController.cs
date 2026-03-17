using UnityEngine;

/// <summary>
/// Camera controller for handling movement, rotation, zoom and target selection
/// </summary>
public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10f; // Camera pan movement speed
    public float rotateSpeed = 100f; // Camera rotation speed
    public float gamerotateSpeed = 5f; // Rotation speed of the target game object
    public Transform target; // The target object to observe
    private Vector3 lastMousePosition; // Stores mouse position from last frame for delta calculation
    public float zoomSpeed = 10f; // Camera zoom speed
    public float minZoom = 1f; // Minimum zoom limit (field of view angle)
    public float maxZoom = 10f; // Maximum zoom limit (field of view angle)

    void Update()
    {
        // Check if left mouse button is pressed (single click)
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit; // Stores raycast detection result
            // Create a ray from main camera to current mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // If ray hits an object and the object has tag "mubiao"
            if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("mubiao"))
            {
                target = hit.transform;   // Set the hit object as current target
            }
        }

        // Get mouse scroll wheel input (positive = forward, negative = backward)
        float zoom = Input.GetAxis("Mouse ScrollWheel");

        // Execute when target object exists
        if (target != null)
        {
            // Check if right mouse button is held down
            if (Input.GetMouseButton(1))
            {
                // Get mouse movement on X axis
                float mouseX = Input.GetAxis("Mouse X");
                // Get mouse movement on Y axis
                float mouseY = Input.GetAxis("Mouse Y");

                // Calculate X axis rotation based on mouse Y movement
                float rotX = mouseY * gamerotateSpeed;
                // Calculate Y axis rotation based on mouse X movement
                float rotY = mouseX * gamerotateSpeed;
                // Note: Rotation angles calculated but not applied to object - unfinished feature
            }
        }

        // Execute when mouse wheel input is significant (filter small scroll movements)
        if (Mathf.Abs(zoom) > 0.01f && Input.GetKey(KeyCode.LeftAlt))
        {
            // Calculate new field of view, clamp within min and max zoom limits
            // Smaller FOV = zoom in, larger FOV = zoom out
            float newZoom = Mathf.Clamp(GetComponent<Camera>().fieldOfView - zoom * zoomSpeed, minZoom, maxZoom);

            // Update camera field of view to achieve zoom effect
            GetComponent<Camera>().fieldOfView = newZoom;
            // Display zoom status text in game manager
            GameManager.ins.Tiop.text = $"Zooming view";
        }
        // Execute camera pan when middle mouse button is held
        if (Input.GetMouseButton(2))
        {
            // Calculate delta between current and last frame mouse position
            Vector3 delta = Input.mousePosition - lastMousePosition;
            // Pan camera based on delta, negative sign corrects movement direction
            transform.Translate(-delta.x * moveSpeed * Time.deltaTime, -delta.y * moveSpeed * Time.deltaTime, 0);
            // Display pan status text in game manager
            GameManager.ins.Tiop.text = $"Panning camera";
        }
        // Execute camera rotation when right mouse button is held
        if (Input.GetMouseButton(1))
        {
            // Calculate horizontal rotation (around Y axis)
            float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
            // Calculate vertical rotation (around X axis)
            float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
            // Rotate camera around target's Y axis (world space) for horizontal rotation
            transform.RotateAround(target.position, Vector3.up, horizontal);
            // Rotate camera around its own X axis for vertical rotation, negative sign corrects direction
            transform.RotateAround(target.position, transform.right, -vertical);
            // Display rotation status text in game manager
            GameManager.ins.Tiop.text = $"Rotating view";
        }

        // Record current mouse position for next frame calculation
        lastMousePosition = Input.mousePosition;
    }
}