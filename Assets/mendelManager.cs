using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class mendelManager : MonoBehaviour
{
    public float totalTime;
    private float currentTime;
    [SerializeField] private TextMeshProUGUI timerText;
    public int score;
    public TextMeshProUGUI scoreText;
    public GameObject gameWinLose, firstQuestion, secondQuestion;

    public int currentQuestion;
    private void Start()
    {
        // Account for game load so it starts exactly where you set it
        totalTime += 1;
        currentTime = totalTime;

        // Initialize the questions
        currentQuestion = 1;
        
    }

    private void Update()
    {
        CountdownTime();
        ScoreCounter();
    }
    void CountdownTime()
    {
        currentTime -= Time.deltaTime;

        currentTime = Mathf.Clamp(currentTime, 0, totalTime);

        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);


        if (currentTime <= 0f)
        {
            //Lose
            gameWinLose.GetComponent<GameWinLose>().timeLeft = currentTime;
            gameWinLose.GetComponent<GameWinLose>().score = score;
            gameWinLose.SetActive(true);
            //Debug.Log("Timer Ran out!");
        }

        if (currentQuestion == 3)
        {
            //win
            gameWinLose.GetComponent<GameWinLose>().timeLeft = currentTime;
            gameWinLose.GetComponent<GameWinLose>().score = score;
            gameWinLose.SetActive(true);
            //Debug.Log("Timer Ran out!");
        }
    }

    void ScoreCounter()
    {
        if(score < 0){score = 0;}
        scoreText.text = score.ToString("D4");
    }

    public void addSCore()
    {
        score +=500;
    }
    public void minusScor()
    {
        score -=100;
    }
    public void displayQuestion()
    {
        if(currentQuestion == 1){firstQuestion.SetActive(true);}
        else{secondQuestion.SetActive(true);}
    }


}
