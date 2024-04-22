using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject pausePanelBlock;
    [SerializeField] private GameObject settingsPanel;

    void Awake()
    {
        pausePanelBlock.SetActive(false);
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void PauseClick()
    {
        TogglePause(true);
    }

    public void ResumeClick()
    {
        TogglePause(false);
    }

    public void SettingsClick()
    {
        settingsPanel.SetActive(true);
        pausePanel.SetActive(false);
    }
    public void SettingsExitClick()
    {
        settingsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void RestartClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitClick()
    {
        SceneManager.LoadScene("main");
    }

    private void TogglePause(bool isPaused)
    {
        
        if(isPaused)
        {
            pausePanelBlock.SetActive(true);
            pausePanel.SetActive(true);
        }

        if(false == isPaused)
        {
            
            pausePanelBlock.SetActive(false);
            pausePanel.SetActive(false);
        }
        Time.timeScale = isPaused ? 0f : 1f; // Pause or resume time
    }
}
