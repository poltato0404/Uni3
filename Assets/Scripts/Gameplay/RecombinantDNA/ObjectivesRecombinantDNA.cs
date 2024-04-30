using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameEssentials.GameManager;

public enum PlayState1
{
    Win,
    Play,
    Lose,
    Wait
}

public class ObjectivesRecombinantDNA1 : MonoBehaviour
{
    // 1st minigame
    public int levelId = 0;

    public PlayState1 currentState;

    public GameObject gameWinLose;

    public int score;
    public TextMeshProUGUI scoreText;

    public int currentQuestion;

    public string[] questions =
    { 
        "Which steps initiates genetic engineering by extracting DNA with the desired gene?",
        "What's the process of inserting a gene into a carrier module?",
        "What's the phase involving transferring the gene-carrying vector into the host organism?",
        "What's the final stage, involving amplifying the gene and identifying successful cells?"
    };
    public TextMeshProUGUI questionsText;

    public static ObjectivesRecombinantDNA1 Instance;

    [Header("Timer Properties")]
    public float totalTime;
    private float currentTime;
    [SerializeField] private TextMeshProUGUI timerText;


    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Start()
    {
        // Account for game load so it starts exactly where you set it
        totalTime += 1;
        currentTime = totalTime;

        // Initialize the questions
        currentQuestion = 0;
        questionsText.text = questions[currentQuestion];
    }

    private void Update()
    {
        CountdownTime1();
    }

    public void SubmitAnswer(string answer, Transform position)
    {
        // Your existing logic for submitting answers
    }

    void CountdownTime1()
    {
        currentTime -= Time.deltaTime;

        currentTime = Mathf.Clamp(currentTime, 0, totalTime);

        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);


        if(currentTime <= 0f)
        {
            //Lose
            gameWinLose.GetComponent<GameWinLose>().timeLeft = currentTime;
            gameWinLose.GetComponent<GameWinLose>().score = score;
            gameWinLose.SetActive(true);
            //Debug.Log("Timer Ran out!");
        }
    }

    void ScoreCounter()
    {
        scoreText.text = score.ToString("D4");
    }
}
