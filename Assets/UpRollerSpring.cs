using UnityEngine;

// 挂在 上滚筒（子物体）
public class UpRollerSpring : MonoBehaviour
{
    [Header("关联把手")]
    public HandleRotator handleRotator;

    [Header("旋转速度")]
    public float rotateSpeed = 100f;

    void Update()
    {
        if (handleRotator == null) return;

        int state = handleRotator.rotateState;

        // 只旋转自己，绝对不跑位
        transform.Rotate(0, -state * rotateSpeed * Time.deltaTime, 0);
    }
}