using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CellObjectives : MonoBehaviour
{
    public static CellObjectives instance;

    public GameObject gameWinLose;
    public int score;
    public saveCS saver;
    public TextMeshProUGUI scoreText;

    [Header("Timer Properties")]
    public float totalTime;
    private float currentTime;
    [SerializeField] private TextMeshProUGUI timerText;

    [HideInInspector] public bool isWin = false;

    public int completedMatches;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentTime = totalTime;
    }


    public void AddMatches(int i, int scoreToAdd)
    {
        completedMatches += i;
        score += scoreToAdd;
        if(score<0){score = 0;}
        CheckCompletion();
    }

    void CheckCompletion()
    {
        if (completedMatches > 5)
        {
            isWin = true;
            saver.finished = isWin;
            saver.score = score;
            gameWinLose.GetComponent<GameWinLose>().timeLeft = currentTime;
            gameWinLose.gameObject.GetComponent<GameWinLose>().score = score;
            
            gameWinLose.SetActive(true);
            
            
        }
    }

    #region TIMER_AND_SCORE
    private void Update()
    {
        saver.finished = isWin;
        saver.score = score;
        ScoreCounter();
        CountdownTimer();
    }

    private void ScoreCounter()
    {
        scoreText.text = score.ToString("D4");
    }

    private void CountdownTimer()
    {
        currentTime -= Time.deltaTime;

        currentTime = Mathf.Clamp(currentTime, 0, totalTime);

        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);


        if (currentTime <= 0f)
        {
            //Lose regardless of score
            gameWinLose.GetComponent<GameWinLose>().timeLeft = currentTime;
            gameWinLose.GetComponent<GameWinLose>().score = score;
            gameWinLose.SetActive(true);
            //Debug.Log("Timer Ran out!");
        }
    }
    #endregion
}