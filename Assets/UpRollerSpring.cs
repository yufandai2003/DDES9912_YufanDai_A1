using UnityEngine;

// Attach to the upper roller (child object)
public class UpRollerSpring : MonoBehaviour
{
    [Header("Linked Handle")]
    public HandleRotator handleRotator;

    [Header("Rotation Speed")]
    public float rotateSpeed = 100f;

    void Update()
    {
        if (handleRotator == null) return;

        int state = handleRotator.rotateState;

        // Only rotate itself, no position shifting
        transform.Rotate(0, -state * rotateSpeed * Time.deltaTime, 0);
    }
}