using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI playerScoreText;

    // Method to update and display the player's score
    public void DisplayScore(int score)
    {
        // Update the text of the TextMeshPro Text element with the player's final score
        playerScoreText.text = "Score: " + score.ToString();
    }

    // Method to toggle the visibility of the score display
    public void SetScoreDisplayActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
