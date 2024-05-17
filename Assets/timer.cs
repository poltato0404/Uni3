using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class leveltimer : MonoBehaviour, IDataPersistence
{
    public float totalTime;
    private float currentTime;
    [SerializeField] private TextMeshProUGUI timerText;

    public void LoadData(GameData data)
    {
        // Initialize the current time to the total time at the start


        switch (data.currentLevel)
        {
            case 1:
                if (data.loadedLevel1) { currentTime = data.currentTime; } else { currentTime = 900f; }
                break;
            case 2:
                if (data.loadedLevel2) { currentTime = data.currentTime; } else { currentTime = 1200f; }
                break;
            case 3:
                if (data.loadedLevel3) { currentTime = data.currentTime; } else { currentTime = 1500f; }
                break;

        }
    }
    public void SaveData(ref GameData data)
    {
        // Initialize the current time to the total time at the start
        data.currentTime = currentTime;
    }

    void Update()
    {
        // Update the countdown timer every frame
        CountdownTime();
    }

    void CountdownTime()
    {
        // Decrease the current time by the time passed since the last frame
        currentTime -= Time.deltaTime;

        // Clamp the current time between 0 and the total time
        currentTime = Mathf.Clamp(currentTime, 0, totalTime);

        // Calculate the minutes and seconds from the current time
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        // Update the timer text in the UI
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Check if the timer has run out
        if (currentTime <= 0f)
        {
            // Timer has run out, handle the event (e.g., trigger lose condition)
            //Debug.Log("Timer Ran out!");
        }
    }



}
