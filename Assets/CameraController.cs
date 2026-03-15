using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10f; // 镜头平移的速度
    public float rotateSpeed = 100f; // 镜头旋转的速度
    public float gamerotateSpeed = 5f; // 游戏对象的旋转速度
    public Transform target; // 被观察的目标物体
    private Vector3 lastMousePosition; // 存储上一帧鼠标的位置，用于计算移动差值
    public float zoomSpeed = 10f; // 镜头缩放的速度
    public float minZoom = 1f; // 镜头最小缩放限制（视野角度）
    public float maxZoom = 10f; // 镜头最大缩放限制（视野角度）

    void Update()
    {
        // 检测鼠标左键是否被按下（按下的瞬间）
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit; // 用于存储射线检测的结果
            // 创建一条从主相机到鼠标当前位置的射线
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // 如果射线与物体碰撞，且碰撞到的物体标签为"mubiao"
            if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("mubiao"))
            {
                target = hit.transform;   // 将碰撞到的物体设置为当前目标
            }
        }

        // 获取鼠标滚轮的输入值（正值为向前滚，负值为向后滚）
        float zoom = Input.GetAxis("Mouse ScrollWheel");

        // 当存在目标物体时
        if (target != null)
        {
            // 检测鼠标右键是否被按住（持续按住状态）
            if (Input.GetMouseButton(1))
            {
                // 获取鼠标在X轴方向的移动量
                float mouseX = Input.GetAxis("Mouse X");
                // 获取鼠标在Y轴方向的移动量
                float mouseY = Input.GetAxis("Mouse Y");

                // 根据鼠标Y轴移动量计算X轴旋转角度
                float rotX = mouseY * gamerotateSpeed;
                // 根据鼠标X轴移动量计算Y轴旋转角度
                float rotY = mouseX * gamerotateSpeed;
                // 注：此处计算了旋转角度但未应用到物体上，可能是未完成的功能
            }
        }

        // 当鼠标滚轮有明显输入时（过滤微小的滚轮动作）
        if (Mathf.Abs(zoom) > 0.01f && Input.GetKey(KeyCode.LeftAlt))
        {
            // 计算新的视野角度，使用Clamp函数限制在最小和最大缩放范围内
            // 视野角度越小，画面放大；视野角度越大，画面缩小
            float newZoom = Mathf.Clamp(GetComponent<Camera>().fieldOfView - zoom * zoomSpeed, minZoom, maxZoom);

            // 更新相机的视野角度，实现缩放效果
            GetComponent<Camera>().fieldOfView = newZoom;
            // 在游戏管理器的提示文本中显示"正在缩放视角"
            GameManager.ins.Tiop.text = $"正在缩放视角";
        }
        // 当同时按住鼠标左键时，执行镜头平移操作
        if (Input.GetMouseButton(2))
        {
            // 计算当前鼠标位置与上一帧鼠标位置的差值
            Vector3 delta = Input.mousePosition - lastMousePosition;
            // 根据差值平移镜头，负号用于纠正移动方向（使镜头跟随鼠标移动）
            transform.Translate(-delta.x * moveSpeed * Time.deltaTime, -delta.y * moveSpeed * Time.deltaTime, 0);
            // 在游戏管理器的提示文本中显示"正在平移镜头"
            GameManager.ins.Tiop.text = $"正在平移镜头";
        }
        // 当同时按住鼠标右键时，执行镜头旋转操作
       
  
          if (Input.GetMouseButton(1))
            {
                // 计算水平方向的旋转量（围绕Y轴）
                float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
                // 计算垂直方向的旋转量（围绕X轴）
                float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
                // 围绕目标物体的Y轴（世界坐标系）旋转镜头（水平旋转）
                transform.RotateAround(target.position, Vector3.up, horizontal);
                // 围绕镜头自身的X轴旋转镜头（垂直旋转），负号用于纠正旋转方向
                transform.RotateAround(target.position, transform.right, -vertical);
                // 在游戏管理器的提示文本中显示"正在旋转视角"
                GameManager.ins.Tiop.text = $"正在旋转视角";
            }
        

        // 记录当前鼠标位置，作为下一帧的"上一帧鼠标位置"
        lastMousePosition = Input.mousePosition;
    }
}