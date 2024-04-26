using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ArchiveVideo : MonoBehaviour
{
    [SerializeField] GameObject video1, video2, video3, video4, video5, video6, video7, video8, video9;
    [SerializeField] List<GameObject> buttons;
    [SerializeField] List<GameObject> backButtons;
    [SerializeField] GameObject buttonForVideo1Panel;
    [SerializeField] GameObject buttonForVideo2Panel;
    public GameObject mainPanel;
    public GameObject easyPanel;
    public GameObject avePanel;
    public GameObject hardPanel;
    [SerializeField] GameObject panelForVideo1Info;
    [SerializeField] GameObject panelForVideo2Info;

    // Declare VideoPlayer variables for each video
    VideoPlayer videoPlayer1;
    VideoPlayer videoPlayer2;
    VideoPlayer videoPlayer3;  // Added VideoPlayers for remaining videos
    VideoPlayer videoPlayer4;
    VideoPlayer videoPlayer5;
    VideoPlayer videoPlayer6;
    VideoPlayer videoPlayer7;
    VideoPlayer videoPlayer8;
    VideoPlayer videoPlayer9;

    void Start()
    {
        videoPlayer1 = video1.GetComponent<VideoPlayer>();
        videoPlayer2 = video2.GetComponent<VideoPlayer>();
        videoPlayer3 = video3.GetComponent<VideoPlayer>();
        videoPlayer4 = video4.GetComponent<VideoPlayer>();
        videoPlayer5 = video5.GetComponent<VideoPlayer>();
        videoPlayer6 = video6.GetComponent<VideoPlayer>();
        videoPlayer7 = video7.GetComponent<VideoPlayer>();
        videoPlayer8 = video8.GetComponent<VideoPlayer>();
        videoPlayer9 = video9.GetComponent<VideoPlayer>();
    }

    public void PlayVideo1()
    {
        video1.SetActive(true);
        videoPlayer1.Play();
        DisableAllButtons();
        buttonForVideo1Panel.SetActive(true);
    }

    public void PlayVideo2()
    {
        video2.SetActive(true);
        videoPlayer2.Play();
        DisableAllButtons();
        buttonForVideo2Panel.SetActive(true);
    }

    public void PlayVideo3()
    {
        video3.SetActive(true);
        videoPlayer3.Play();
        DisableAllButtons();
    }

    public void PlayVideo4()
    {
        video4.SetActive(true);
        videoPlayer4.Play();
        DisableAllButtons();
    }

    public void PlayVideo5()
    {
        video5.SetActive(true);
        videoPlayer5.Play();
        DisableAllButtons();
    }

    public void PlayVideo6()
    {
        video6.SetActive(true);
        videoPlayer6.Play();
        DisableAllButtons();
    }

    public void PlayVideo7()
    {
        video7.SetActive(true);
        videoPlayer7.Play();
        DisableAllButtons();
    }

    public void PlayVideo8()
    {
        video8.SetActive(true);
        videoPlayer8.Play();
        DisableAllButtons();
    }

    public void PlayVideo9()
    {
        video9.SetActive(true);
        videoPlayer9.Play();
        DisableAllButtons();
    }

    void DisableAllButtons()
    {
        foreach (var button in buttons)
        {
            button.SetActive(false);
        }
    }

    public void ShowVideo1Panel()
    {
        panelForVideo1Info.SetActive(true);
        buttonForVideo1Panel.SetActive(false);
        videoPlayer1.Stop();
        video1.SetActive(false);
        backButtons[9].SetActive(true);
    }

    public void ShowVideo2Panel()
    {
        panelForVideo2Info.SetActive(true);
        buttonForVideo2Panel.SetActive(false);
        videoPlayer2.Stop();
        video2.SetActive(false);
        backButtons[10].SetActive(true);
    }
    public void GoBackFromVideo1()
    {
        panelForVideo1Info.SetActive(false);
        buttonForVideo1Panel.SetActive(false);
        backButtons[0].SetActive(false);

        video1.SetActive(false);
        videoPlayer1.Stop();

        EnableAllButtons();

        easyPanel.SetActive(true);
    }

    public void GoBackFromVideo2()
    {
        panelForVideo2Info.SetActive(false);
        buttonForVideo2Panel.SetActive(false);
        backButtons[1].SetActive(false);

        video2.SetActive(false);
        videoPlayer2.Stop();

        EnableAllButtons();

        easyPanel.SetActive(true);
    }

    public void GoBackFromVideo3()
    {
        backButtons[2].SetActive(false);

        video3.SetActive(false);
        videoPlayer3.Stop();

        EnableAllButtons();

        easyPanel.SetActive(true);
    }

    public void GoBackFromVideo4()
    {
        backButtons[3].SetActive(false);

        video4.SetActive(false);
        videoPlayer4.Stop();

        EnableAllButtons();

        avePanel.SetActive(true);
    }

    public void GoBackFromVideo5()
    {
        backButtons[4].SetActive(false);

        video5.SetActive(false);
        videoPlayer5.Stop();

        EnableAllButtons();

        avePanel.SetActive(true);
    }

    public void GoBackFromVideo6()
    {
        backButtons[5].SetActive(false);

        video6.SetActive(false);
        videoPlayer6.Stop();

        EnableAllButtons();

        avePanel.SetActive(true);
    }

    public void GoBackFromVideo7()
    {
        backButtons[6].SetActive(false);

        video7.SetActive(false);
        videoPlayer5.Stop();

        EnableAllButtons();

        hardPanel.SetActive(true);
    }

    public void GoBackFromVideo8()
    {
        backButtons[7].SetActive(false);

        video8.SetActive(false);
        videoPlayer8.Stop();

        EnableAllButtons();

        hardPanel.SetActive(true);
    }

    public void GoBackFromVideo9()
    {
        backButtons[8].SetActive(false);

        video9.SetActive(false);
        videoPlayer9.Stop();

        EnableAllButtons();

        hardPanel.SetActive(true);
    }

    public void GoBackFromPanel1Info()
    {
        backButtons[9].SetActive(false);

        panelForVideo1Info.SetActive(false);

        EnableAllButtons();

        easyPanel.SetActive(true);
    }

    public void GoBackFromPanel2Info()
    {
        backButtons[10].SetActive(false);

        panelForVideo2Info.SetActive(false);

        EnableAllButtons();

        easyPanel.SetActive(true);
    }

    void EnableAllButtons()
    {
        foreach (var button in buttons)
        {
            button.SetActive(true);
        }

        buttonForVideo1Panel.SetActive(true);
        buttonForVideo2Panel.SetActive(true);

        foreach (var backButton in backButtons)
        {
            backButton.SetActive(true);
        }
    }
}