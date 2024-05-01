using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject mainPanel; // Add this line for the main panel
    public GameObject tileList;
    public Animator playerAnimator;
    public GameObject dataList;
    public GameObject panelTimer;
    public GameObject panelScore;

    void Awake()
    {
        panelTimer.SetActive(false);
        panelScore.SetActive(false);
        mainPanel.SetActive(false); // Initially hide the main panel
        dataList = GameObject.Find("DataScript");
    }

    public void StartGame()
    {
        playerAnimator.SetBool("isGameStart", true);
        startPanel.SetActive(false);

        // Activate main panel after start panel hides
        mainPanel.SetActive(true);

        tileList.SetActive(true);
        dataList.GetComponent<DataScript>().getQuestion();
        panelTimer.SetActive(true);
        panelScore.SetActive(true);
        StartCoroutine(GetComponent<TimerScript>().startTimer());
    }
}
