using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject pausePanelBlock;

    void Awake()
    {
        pausePanelBlock.SetActive(false);
        pausePanel.SetActive(false);
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
        SceneManager.LoadScene("SettingsScene");
    }

    public void RestartClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitClick()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    private void TogglePause(bool isPaused)
    {
        Time.timeScale = isPaused ? 0f : 1f; // Pause or resume time
        pausePanelBlock.SetActive(isPaused);
        pausePanel.SetActive(isPaused);
    }
}
