using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject pausePanel, pausePanelBlock, settingsPanel, menuPanel;

    void Awake()
    {
        pausePanelBlock.SetActive(false);
        menuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void pauseClick()
    {
        togglePauseResume();
        pausePanelBlock.SetActive(true);
        pausePanel.GetComponent<Animator>().SetBool("isPaused", true);
    }

    public void resumeClick()
    {
        togglePauseResume();
        pausePanelBlock.SetActive(false);
        pausePanel.GetComponent<Animator>().SetBool("isPaused", false);
    }

    public void settingsClick()
    {
        menuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void backSettingsClick()
    {
        menuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void quitClick()
    {
        Application.Quit();
    }

    public void restartClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene");
    }

    public void togglePauseResume()
    {
        var timerScript = GameObject.Find("GameManager").GetComponent<TimerScript>();
        GameObject.Find("GameManager").GetComponent<TimerScript>().stopTime = !timerScript.stopTime;
        StartCoroutine(timerScript.startTimer());
    }
}
