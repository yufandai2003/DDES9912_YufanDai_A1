using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ClothMoveSimple : MonoBehaviour
{
    public HandleRotator handleRotator;
    public float moveSpeed = 0.6f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Enable kinematic rigidbody
    }

    void FixedUpdate() // Physics movement must be in FixedUpdate
    {
        if (handleRotator == null) return;
        int state = handleRotator.rotateState;

        // Calculate target position
        Vector3 movement = new Vector3(0, 0, -state * moveSpeed * Time.fixedDeltaTime);

        // Use MovePosition instead of Translate
        rb.MovePosition(rb.position + movement);
    }
}