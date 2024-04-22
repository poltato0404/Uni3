using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameStarter : MonoBehaviour
{
    public int minigameID;

    public TextMeshProUGUI minigameTitle, 
                           minigameDesc;


    public List<GameObject> disabledGameobjects;


    public string[] minigameTitles =
    {
        "Recombinant DNA",
        "Feedback Mechanism",
        "Central Dogma of Molecular Biology (DNA)"
    };

    [TextArea(10, 30)]
    public string[] minigameInfo =
    {
        "",
        "",
        ""
    };

    [TextArea(10, 30)]
    public string[] introductionText;
    public TextMeshProUGUI introText;

    public GameObject introPanelScreen;

    public void Start()
    {
        // In case there might be some residual text
        minigameTitle.text = "";
        minigameDesc.text = "";
        OnInitiate();
    }

    public void OnInitiate()
    {
        switch (minigameID)
        {
            case 0:
                minigameTitle.text = minigameTitles[0];
                minigameDesc.text = minigameInfo[0];
                introText.text = introductionText[0];
                break;
            case 1:
                minigameTitle.text = minigameTitles[1];
                minigameDesc.text = minigameInfo[1];
                introText.text = introductionText[1];
                break;
            case 2:
                minigameTitle.text = minigameTitles[2];
                minigameDesc.text = minigameInfo[2];
                introText.text = introductionText[2];
                break;
            case 3:
                minigameTitle.text = minigameTitles[3];
                minigameDesc.text = minigameInfo[3];
                introText.text = introductionText[3];
                break;
            case 4:
                minigameTitle.text = minigameTitles[4];
                minigameDesc.text = minigameInfo[4];
                introText.text = introductionText[4];
                break;
            case 5:
                minigameTitle.text = minigameTitles[5];
                minigameDesc.text = minigameInfo[5];
                introText.text = introductionText[5];
                break;
            case 6:
                minigameTitle.text = minigameTitles[6];
                minigameDesc.text = minigameInfo[6];
                introText.text = introductionText[6];
                break;
            default: // Just in case
                break;
        }

        introPanelScreen.SetActive(false);
        Time.timeScale = 0.0f;
    }

    public void OnClick_PlayButton()
    {
        if(disabledGameobjects.Count > 0)
            foreach(GameObject gameObject in disabledGameobjects)
            {
                gameObject.SetActive(true);
            }
        this.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void OnClick_QuitButton()
    {
        // Just in case the time scale gets carried over the scene, we reset it back
        Time.timeScale = 1.0f; 
        SceneManager.LoadScene(0);
    }
}