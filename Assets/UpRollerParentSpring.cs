using UnityEngine;

// 挂在 上滚筒的父物体 UpRollerParent
public class UpRollerParentSpring : MonoBehaviour
{
    [Header("弹簧目标高度（最低点=初始位置）")]
    public float targetY = 0f;

    [Header("弹簧强度")]
    public float springForce = 50f;

    [Header("阻尼")]
    public float damping = 5f;

    [Header("跳动上限：比最低点高 0.02")]
    public float maxOffsetY = 0.02f;

    [Header("跳动频率（越小跳得越慢）")]
    public float shakeInterval = 0.15f;

    [Header("当前是否允许跳动")]
    private float shakeTimer;

    private Rigidbody rb;
    private float baseY; // 初始基准位置

    void Start()
    {
        baseY = transform.position.y;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    }

    void FixedUpdate()
    {
        float yDelta = targetY - transform.position.y;
        rb.velocity = new Vector3(0, yDelta * springForce - rb.velocity.y * damping, 0);
    }

    void Update()
    {
        var handle = GetComponentInChildren<UpRollerSpring>()?.handleRotator;
        if (handle == null || handle.rotateState == 0)
        {
            shakeTimer = 0;
            return;
        }

        // 控制跳动频率 ← 核心
        shakeTimer += Time.deltaTime;
        if (shakeTimer >= shakeInterval)
        {
            shakeTimer = 0;

            // 在 初始位置 ~ 初始位置+0.02 之间轻柔跳动
            float randomY = Random.Range(baseY, baseY + maxOffsetY);
            transform.position = new Vector3(transform.position.x, randomY, transform.position.z);
        }
    }
}