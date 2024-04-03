using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameEssentials.GameManager;

public enum PlayState
{
    Win,
    Play,
    Lose,
    Wait
}

public class ObjectivesRecombinantDNA : MonoBehaviour
{
    // 1st minigame
    public int levelId = 0;

    public PlayState currentState;

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

    public static ObjectivesRecombinantDNA Instance;

    public GameObject rightPrefab, wrongPrefab;

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
        CountdownTime();
        ScoreCounter();
    }

    public void SubmitAnswer(string answer, Transform position)
    {
        switch (currentQuestion)
        {
            case 0:
                if(answer == "DNA Isolation")
                {
                    // Correct answer
                    currentQuestion = 1;
                    questionsText.text = questions[currentQuestion];
                    Instantiate(rightPrefab, position.position, Quaternion.identity);
                    score += 10;
                }
                else if(answer == "Bomb")
                {
                    // Lose
                }
                else
                {
                    Instantiate(wrongPrefab, position.position, Quaternion.identity);
                    if (score <= 0)
                        score = 0;
                    else
                        score -= 5;
                }
                break;
            case 1:
                if (answer == "Gene Insertion")
                {
                    // Correct answer
                    currentQuestion = 2;
                    questionsText.text = questions[currentQuestion];
                    Instantiate(rightPrefab, position.position, Quaternion.identity);
                    score += 10;
                }
                else if (answer == "Bomb")
                {
                    // Lose
                }
                else
                {
                    Instantiate(wrongPrefab, position.position, Quaternion.identity);
                    if (score <= 0)
                        score = 0;
                    else
                        score -= 5;
                }
                break;
            case 2:
                if (answer == "Vector Transfer")
                {
                    // Correct answer
                    currentQuestion = 3;
                    questionsText.text = questions[currentQuestion];
                    Instantiate(rightPrefab, position.position, Quaternion.identity);
                    score += 10;
                }
                else if (answer == "Bomb")
                {
                    // Lose
                }
                else
                {
                    Instantiate(wrongPrefab, position.position, Quaternion.identity);
                    if (score <= 0)
                        score = 0;
                    else
                        score -= 5;
                }
                break;
            case 3:
                if (answer == "Cloning and Screening")
                {
                    // Correct answer
                    currentQuestion = 0;
                    questionsText.text = questions[currentQuestion];
                    Instantiate(rightPrefab, position.position, Quaternion.identity);
                    score += 10;
                    if (score > 0)
                    {
                        GameManager.Instance.isLevelComplete[levelId] = true;
                    }
                    gameWinLose.gameObject.GetComponent<GameWinLose>().score = score;
                    gameWinLose.SetActive(true);
                }
                else if (answer == "Bomb")
                {
                    // Lose
                }
                else
                {
                    Instantiate(wrongPrefab, position.position, Quaternion.identity);
                    if (score <= 0)
                        score = 0;
                    else
                        score -= 5;
                }
                break;
            default:
                return;
        }
    }

    void CountdownTime()
    {
        currentTime -= Time.deltaTime;

        currentTime = Mathf.Clamp(currentTime, 0, totalTime);

        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);


        if(currentTime <= 0f)
        {
            //Lose

            gameWinLose.SetActive(true);
            //Debug.Log("Timer Ran out!");
        }
    }

    void ScoreCounter()
    {
        scoreText.text = score.ToString("D4");
    }
}