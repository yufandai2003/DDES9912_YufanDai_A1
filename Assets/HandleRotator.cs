using UnityEngine;
using UnityEngine.UI; // 必须加

public class HandleRotator : MonoBehaviour
{
    [Header("输出滚筒")]
    public Transform downRoller;

    [Header("传动比 把手:滚筒 = 1:2")]
    public float gearRatio = 2f;

    [Header("滚轮灵敏度")]
    public float wheelSensitivity = 20f;

    [Header("UI按钮控制转速")]
    public float autoRotateSpeed = 100f;

    // 自动旋转状态：0停止 1正转 -1反转
    public  int rotateState = 0;

    void Update()
    {
        // ====================== 滚轮控制（原有功能） ======================
        if (IsMouseOverObject())
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
            {
                rotateState = 0; // 滚轮一动，自动停止

                transform.Rotate(0, scroll * wheelSensitivity, 0);
                if (downRoller != null)
                    downRoller.Rotate(0, scroll * wheelSensitivity * gearRatio, 0);
            }
        }

        // ====================== UI按钮 自动控制 ======================
        if (rotateState != 0)
        {
            float rotateAmount = rotateState * autoRotateSpeed * Time.deltaTime;
            transform.Rotate(0, rotateAmount, 0);
            if (downRoller != null)
                downRoller.Rotate(0, rotateAmount * gearRatio, 0);
        }
    }

    // ====================== UI按钮调用的方法 ======================
    public void OnForwardBtn() => rotateState = 1;   // 正转
    public void OnReverseBtn() => rotateState = -1;  // 反转
    public void OnStopBtn() => rotateState = 0;      // 停止

    // 检测鼠标是否在物体上
    private bool IsMouseOverObject()
    {
        if (Camera.main == null) return false;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
            return hit.transform == transform;
        return false;
    }
}