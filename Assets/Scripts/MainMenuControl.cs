using GameEssentials.GameManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuControl : MonoBehaviour
 {
    public MinigamesButton[] buttons;

    private void Start()
    {
        // Commented to prevent null referencing errors
        /*
        int buttonsToActivate = 0;

        foreach (bool levels in GameManager.Instance.isLevelComplete)
        {
            if (levels == true)
                buttonsToActivate++;
        }

        if(buttonsToActivate == 0)
        {
            buttons[0].button.interactable = true;
            buttons[0].lockImage.gameObject.SetActive(false);
            buttons[0].splashImage.gameObject.SetActive(true);
            buttons[0].textObject.SetActive(true);
        }
        else
        {
            for (int i = 0; i <= buttonsToActivate; i++)
            {
                buttons[i].button.interactable = true;
                buttons[i].lockImage.gameObject.SetActive(false);
                buttons[i].splashImage.gameObject.SetActive(true);
                buttons[i].textObject.SetActive(true);
            }
        }
        */
    }


     public void OnClick_Start(int whichLevelToLoad)
    {
        SceneManager.LoadScene(1);
         GameManager.Instance.sceneToLoad = whichLevelToLoad;
    }

    public void OnClick_Options()
    {
    //might not be needed but only for sound options
    }

    public void OnClick_Quit()
    {
    // run a save data first before quitting
        Application.Quit();
    }
}

[System.Serializable]
public class MinigamesButton
{
    public Button button;
    public GameObject textObject;
    public Image splashImage;
    public Image lockImage;
}