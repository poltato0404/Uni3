using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class quizmanagerBUTTON : MonoBehaviour
{
    public List<CategoryBtnScript> categoryButtons;
    public int totalScore;
    public TextMeshProUGUI itext;
    public Button interactableButton;

    private void Start()
    {
        // Optionally, you could find all CategoryBtnScript instances dynamically

        itext = GetComponent<TextMeshProUGUI>();
        // Ensure the button is disabled initially
        if (interactableButton != null)
        {
            interactableButton.interactable = false;
        }
    }

    public int GetTotalScore()
    {
        int totalScore = 0;

        foreach (var categoryBtn in categoryButtons)
        {
            totalScore += categoryBtn.GetScore();
        }

        return totalScore;
    }

    void Update()
    {
        categoryButtons = new List<CategoryBtnScript>(FindObjectsOfType<CategoryBtnScript>());
        totalScore = GetTotalScore();
        itext.text = ("Total Score: " + totalScore.ToString());
        Debug.Log("Total Score: " + totalScore);
        if (totalScore >= 10)
        {

            interactableButton.interactable = true;

        }
    }
}
