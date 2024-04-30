using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GameEssentials.GameManager;

public class QuestManager : MonoBehaviour
{
    // 2nd minigame
    public int levelId = 1;

    public GameObject gameWinLose;
    public GaugeMeter meter;

    public int score;
    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI questText;
    public int completedQuest;
    public int targetQuest;
    public string currentQuestText;

    public GameObject[] QuestHolder;

    [TextArea(1,3)]
    public string[] questString =
    {
        "You're body is heating up too quickly. Find a shade quickly so you don't overheat !",
        "You're in the heat quite a while, you're body is slowly getting thirsty",
        "Don't go in that hot spring, the water may seem tempting but it got hot because of the sun!",
        ""
    };

    public float delayBetweenCharacters = 0.1f;

    [Header("Timer Properties")]
    public float totalTime;
    private float currentTime;
    [SerializeField] private TextMeshProUGUI timerText;

    public static QuestManager Instance;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }


    private void Start()
    {
        questText.text = "";
        completedQuest = 0;

        // Account for game load so it starts exactly where you set it
        totalTime += 1;
        currentTime = totalTime;

        currentQuestText = questString[completedQuest];
        StartCoroutine(ShowText(questString[completedQuest]));
    }

    private void OnEnable()
    {
        meter.RotateToRight();
    }

    private void Update()
    {
        CountdownTimer();
        ScoreCounter();

        if (completedQuest >= targetQuest)
        {
            gameWinLose.GetComponent<GameWinLose>().timeLeft = currentTime;
            gameWinLose.GetComponent<GameWinLose>().score = score;
            targetQuest = 4;
            gameWinLose.SetActive(true);
        }
    }

    void CountdownTimer()
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
    }

    void ScoreCounter()
    {
        scoreText.text = score.ToString("D4");
    }

    public void ResetQuest()
    {
        completedQuest = 0;
    }


    public void ProceedQuest(int currentQuestNum)
    {
        if (completedQuest < targetQuest)
        {
            if (currentQuestNum == 0) meter.RotateToCenter();
            
            currentQuestText = questString[currentQuestNum];
            StartCoroutine(ShowText(currentQuestText));
            completedQuest++;
        }

        /*
        if(completedQuest == currentQuestNum)
        {
            if (completedQuest == 2)
            {
                if (score > 0)
                {
                    GameManager.Instance.isLevelComplete[levelId] = true;
                }

                gameWinLose.GetComponent<GameWinLose>().timeLeft = currentTime;
                gameWinLose.GetComponent<GameWinLose>().score = score;
                gameWinLose.SetActive(true);
                return;
            }

            if (completedQuest == 0) meter.RotateToCenter();

            questText.text = "";

            if (completedQuest < 2)
            {
                QuestHolder[completedQuest].SetActive(false);
                completedQuest += 1;
                QuestHolder[completedQuest].SetActive(true);
            }
                
            else
                completedQuest = 0;

            currentQuestText = questString[completedQuest];

            StartCoroutine(ShowText(currentQuestText));
        }
        */
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        if (score <= 0)
            score = 0;
    }


    IEnumerator ShowText(string text)
    {
        questText.text = "";

        for (int i = 0; i < text.Length; i++)
        {
            questText.text += text[i];
            yield return new WaitForSeconds(delayBetweenCharacters);
        }
    }
}