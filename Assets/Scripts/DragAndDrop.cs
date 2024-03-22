using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class DragAndDropGame : MonoBehaviour
{
    public List<DraggableItem> draggableItems;
    public List<GameObject> targetObjects;
    public TMP_Text scoreText; // Reference to TextMeshPro Text element for displaying score
    public TMP_Text timerText; // Reference to TextMeshPro Text element for displaying timer
    public ScoreDisplay scoreDisplay; // Reference to the ScoreDisplay script

    private bool isGameActive = false;
    private int score = 0;
    private float timer = 25f;
    private bool allItemsMatched = false;

    void Start()
    {
        for (int i = 0; i < draggableItems.Count; i++)
        {
            if (i < targetObjects.Count)
            {
                draggableItems[i].Initialize(this, targetObjects[i]);
            }
            else
            {
                Debug.LogError("Not enough target objects for draggable items!");
            }
        }
    }

    void Update()
    {
        if (isGameActive)
        {
            if (timer > 0f)
            {
                timer -= Time.deltaTime;
                UpdateTimerUI(); // Update timer UI
                Debug.Log("Timer: " + timer); // Debug log for timer
            }
            else
            {
                GameOver(); // End the game when the timer reaches 0
            }

            // Check if all items are matched
            if (AreAllItemsMatched())
            {
                isGameActive = false; // Stop the game
                Debug.Log("All items matched!"); // Debug log for all items matched
            }
        }
    }


    public void StartGame()
    {
        isGameActive = true;
        UpdateScoreUI(); // Update score UI
        UpdateTimerUI(); // Update timer UI
    }
    public void CorrectMatch(DraggableItem matchedItem)
    {
        score += 10;
        UpdateScoreUI(); // Update score UI

        if (AreAllItemsMatched()) // Check if all items are matched
        {
            isGameActive = false; // Stop the game
            GameOver(); // End the game
        }
    }

    private bool AreAllItemsMatched()
    {
        foreach (var item in draggableItems)
        {
            if (!item.IsMatched) // If any item is not matched, return false
            {
                return false;
            }
        }
        return true; // All items are matched
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void UpdateTimerUI()
    {
        float displayedTime = Mathf.Max(0f, timer); // Ensure that the displayed time is not negative
        timerText.text = "Time: " + Mathf.Ceil(displayedTime).ToString();
    }

    void GameOver()
    {
        isGameActive = false;
        Debug.Log("Game Over - Initial Timer Value: " + timer); // Debug log the initial timer value
        timer = Mathf.Max(0f, timer); // Ensure the timer value is not negative
        Debug.Log("Game Over - Timer After Clamping: " + timer); // Debug log the timer value after clamping
        Debug.Log("Game Over - Final Timer Value: " + timer); // Debug log the final timer value
        UpdateTimerUI(); // Update timer UI
        scoreDisplay.DisplayScore(score); // Display the player's final score
        scoreDisplay.SetScoreDisplayActive(true); // Activate the score panel GameObject
    }

}
