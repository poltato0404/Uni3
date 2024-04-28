using GameEssentials.GameManager;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public enum Game
{
    A,
    B
}


public class OrganMatchObjectives : MonoBehaviour
{
    // 5th minigame
    public int levelId = 4;

    public GameObject gameWinLose;

    public int matches = 0;
    public TextMeshProUGUI matchesText;

    public static OrganMatchObjectives instance;

    public TextMeshProUGUI questText;

    public int score;
    public TextMeshProUGUI scoreText;

    public GameObject popupriddleUI;
    public TextMeshProUGUI popupUIText;
    [TextArea(3, 15)]
    public string[] riddleText;

    public float delayBetweenCharacters;

    [Header("Timer Properties")]
    public float totalTime;
    private float currentTime;
    [SerializeField] private TextMeshProUGUI timerText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        currentTime = totalTime;
        ShowRiddle(0);
    }

    private void Update()
    {
        UpdateMatches();
        UpdateScore();
        UpdateTimer();
    }

    void UpdateMatches()
    {
        matchesText.text = matches + "/7";

        if (matches >= 7)
        {
            gameWinLose.GetComponent<GameWinLose>().timeLeft = currentTime;
            gameWinLose.GetComponent<GameWinLose>().score = score;
            gameWinLose.SetActive(true);
        }
    }

    void UpdateScore()
    {
        scoreText.text = score.ToString("D4");
    }

    void UpdateTimer()
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

    public void AddMatches(int _matches)
    {
        matches += _matches;
    }

    public void ShowRiddle(int index)
    {
        StartCoroutine(showRiddleDelayCo(index));
    }

    IEnumerator showRiddleDelayCo(int index)
    {
        yield return new WaitForSeconds(0.5f);

        if (matches < 7)
        {
            popupUIText.text = "";
            popupriddleUI.SetActive(true);
            popupUIText.text = riddleText[index];
        }
    }
    public void ShowDescText(string text)
    {
        StartCoroutine(ShowText(text));
    }

    IEnumerator ShowText(string text)
    {
        questText.text = "";

        for (int i = 0; i < text.Length; i++)
        if (matches < 7)
        {
            questText.text += text[i];
            yield return new WaitForSeconds(delayBetweenCharacters);
        }

        if (matches >= 7)
        {
            if (score > 0)
            {
                GameManager.Instance.isLevelComplete[levelId] = true;
            }
            gameWinLose.GetComponent<GameWinLose>().timeLeft = currentTime;
            gameWinLose.GetComponent<GameWinLose>().score = score;
            gameWinLose.SetActive(true);
            popupUIText.text = "";
        }
    }
}