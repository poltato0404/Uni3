using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript2 : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject pausePanelBlock;
    public GameObject mainPanel;
    [SerializeField] private GameObject settingsPanel;

    public GameObject archivepanel;
    void Awake()
    {
        pausePanelBlock.SetActive(false);
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
        mainPanel.SetActive(false);
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

    public void ArchiveClick()
    {
        mainPanel.SetActive(true);
        archivepanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void QuitClick()
    {
        SceneManager.LoadScene("main");
    }

    public void openTerminal()
    {
        Time.timeScale = 0f;
    }
    public void closeTerminal()
    {
        Time.timeScale = 1f;
    }

    private void TogglePause(bool isPaused)
    {

        if (isPaused)
        {
            pausePanelBlock.SetActive(true);
            pausePanel.SetActive(true);
        }

        if (false == isPaused)
        {

            pausePanelBlock.SetActive(false);
            pausePanel.SetActive(false);
        }
        Time.timeScale = isPaused ? 0f : 1f; // Pause or resume time
    }
}
