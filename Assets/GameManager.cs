using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 引入场景管理命名空间

public class GameManager : MonoBehaviour
{
    public static GameManager ins; // 单例实例，用于全局访问
    public Text Tiop; // 用于显示操作提示的文本组件
    public GameObject panel; // 初始面板（可能是开场说明或加载面板）
    public Text tishitext; // 提示文本组件，用于显示说明文字
    public string yuedu; // 要显示的阅读文本内容
    public bool startBool;
    private void Awake()
    {
        ins = this; // 在对象唤醒时初始化单例，确保全局唯一
        panel.SetActive(false); // 关闭初始面板
    }

   

    // Update is called once per frame
    void Update()
    {
        // 如果初始面板处于激活状态
        if (panel.activeSelf)
        {
            // 检测到任何按键或鼠标点击
            if (Input.anyKeyDown)
            {
                panel.SetActive(false); // 关闭初始面板
            }
        }
    }

    public void chongzhi()//重新开始实验
    {
        // 重新加载当前场景，实现重置功能
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}