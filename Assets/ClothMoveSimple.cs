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
        rb.isKinematic = true; // 开启运动学刚体
    }

    void FixedUpdate() // 物理移动必须在 FixedUpdate
    {
        if (handleRotator == null) return;
        int state = handleRotator.rotateState;

        // 计算目标位置
        Vector3 movement = new Vector3(0, 0, -state * moveSpeed * Time.fixedDeltaTime);

        // 使用 MovePosition 而不是 Translate
        rb.MovePosition(rb.position + movement);
    }
}