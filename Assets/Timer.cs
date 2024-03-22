using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    private float countdown = 60.0f; // Initial countdown time (adjust as needed)

    private void Update()
    {
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            // Timer has reached zero (implement your logic here)
            Debug.Log("Time's up!");
        }
    }

    private void UpdateTimerDisplay()
    {
        // Display the countdown in minutes and seconds
        int minutes = Mathf.FloorToInt(countdown / 60);
        int seconds = Mathf.FloorToInt(countdown % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
