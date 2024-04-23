using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video; // Import the Video module

public class ArchiveVideo : MonoBehaviour
{
    [SerializeField] GameObject video1, video2, video3, video4, video5, video6, video7, video8, video9;
    [SerializeField] List<GameObject> buttons;
    [SerializeField] GameObject buttonForVideo1Panel;
    [SerializeField] GameObject buttonForVideo2Panel;
    [SerializeField] GameObject panelForVideo1Info;
    [SerializeField] GameObject panelForVideo2Info;

    // Declare VideoPlayer variables for each video
    VideoPlayer videoPlayer1;
    VideoPlayer videoPlayer2;

    // Start is called before the first frame update
    void Start()
    {
        // Get VideoPlayer components from the video GameObjects
        videoPlayer1 = video1.GetComponent<VideoPlayer>();
        videoPlayer2 = video2.GetComponent<VideoPlayer>();
    }

    public void PlayVideo1()
    {
        video1.SetActive(true);
        videoPlayer1.Play(); // Play video 1
        DisableAllButtons();
        buttonForVideo1Panel.SetActive(true);
    }

    public void PlayVideo2()
    {
        video2.SetActive(true);
        videoPlayer2.Play(); // Play video 2
        DisableAllButtons();
        buttonForVideo2Panel.SetActive(true);
    }

    public void PlayVideo3()
    {
        video3.SetActive(true);
        DisableAllButtons();
    }

    public void PlayVideo4()
    {
        video4.SetActive(true);
        DisableAllButtons();
    }

    public void PlayVideo5()
    {
        video5.SetActive(true);
        DisableAllButtons();
    }

    public void PlayVideo6()
    {
        video6.SetActive(true);
        DisableAllButtons();
    }

    public void PlayVideo7()
    {
        video7.SetActive(true);
        DisableAllButtons();
    }

    public void PlayVideo8()
    {
        video8.SetActive(true);
        DisableAllButtons();
    }

    public void PlayVideo9()
    {
        video9.SetActive(true);
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
        buttonForVideo1Panel.SetActive(false); // Disable button after clicking
        videoPlayer1.Stop(); // Stop video 1
    }

    public void ShowVideo2Panel()
    {
        panelForVideo2Info.SetActive(true);
        buttonForVideo2Panel.SetActive(false); // Disable button after clicking
        videoPlayer2.Stop(); // Stop video 2
    }
}
