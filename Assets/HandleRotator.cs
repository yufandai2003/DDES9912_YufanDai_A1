using UnityEngine;
using UnityEngine.UI; // Required import

public class HandleRotator : MonoBehaviour
{
    [Header("Output Roller")]
    public Transform downRoller;

    [Header("Gear Ratio Handle:Roller = 1:2")]
    public float gearRatio = 2f;

    [Header("Mouse Wheel Sensitivity")]
    public float wheelSensitivity = 20f;

    [Header("UI Button Rotation Speed")]
    public float autoRotateSpeed = 100f;

    // Auto rotation state: 0 = Stop, 1 = Forward, -1 = Reverse
    public int rotateState = 0;

    void Update()
    {
        // ====================== Mouse Wheel Control (Original Feature) ======================
        if (IsMouseOverObject())
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
            {
                rotateState = 0; // Stop auto rotation when wheel is used

                transform.Rotate(0, scroll * wheelSensitivity, 0);
                if (downRoller != null)
                    downRoller.Rotate(0, scroll * wheelSensitivity * gearRatio, 0);
            }
        }

        // ====================== UI Button Auto Control ======================
        if (rotateState != 0)
        {
            float rotateAmount = rotateState * autoRotateSpeed * Time.deltaTime;
            transform.Rotate(0, rotateAmount, 0);
            if (downRoller != null)
                downRoller.Rotate(0, rotateAmount * gearRatio, 0);
        }
    }

    // ====================== Methods Called by UI Buttons ======================
    public void OnForwardBtn() => rotateState = 1;   // Forward rotation
    public void OnReverseBtn() => rotateState = -1;  // Reverse rotation
    public void OnStopBtn() => rotateState = 0;      // Stop rotation

    // Check if mouse is hovering over this object
    private bool IsMouseOverObject()
    {
        if (Camera.main == null) return false;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
            return hit.transform == transform;
        return false;
    }
}