using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Import scene management namespace

public class GameManager : MonoBehaviour
{
    public static GameManager ins; // Singleton instance for global access
    public Text Tiop; // Text component for displaying operation tips
    public GameObject panel; // Initial panel (intro or loading panel)
    public Text tishitext; // Text component for displaying instructions
    public string yuedu; // Content of the reading text to display
    public bool startBool;

    private void Awake()
    {
        ins = this; // Initialize singleton on awake to ensure global uniqueness
        panel.SetActive(false); // Deactivate initial panel
    }

    // Update is called once per frame
    void Update()
    {
        // If initial panel is active
        if (panel.activeSelf)
        {
            // Detect any key or mouse click
            if (Input.anyKeyDown)
            {
                panel.SetActive(false); // Hide initial panel
            }
        }
    }

    public void chongzhi() // Restart the experiment
    {
        // Reload current scene to reset
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}