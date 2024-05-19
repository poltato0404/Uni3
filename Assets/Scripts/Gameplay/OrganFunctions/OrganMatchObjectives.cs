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
    public SaveOS saver;

    public int levelId = 4;

    public GameObject gameWinLose;

    public int matches = 0;
    public TextMeshProUGUI matchesText;

    public static OrganMatchObjectives instance;

    public TextMeshProUGUI questText;

    public int score;
    public TextMeshProUGUI scoreText;
    public bool finished = true;

    public GameObject popupriddleUI;
    public TextMeshProUGUI popupUIText;
    [TextArea(3, 15)]
    public string[] riddleText;

    public AudioClip[] riddleAudioClips; // Array to hold audio clips
    private AudioSource audioSource; // Reference to the AudioSource component

    public float delayBetweenCharacters;

    [Header("Timer Properties")]
    public float totalTime;
    private float currentTime;
    [SerializeField] private TextMeshProUGUI timerText;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        audioSource = GetComponent<AudioSource>(); // Initialize the AudioSource component
    }

    private void Start()
    {
        currentTime = totalTime;
        ShowRiddle(0);
    }

    private void Update()
    {
        saver.finished = finished;
        saver.score = score;
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

            // Play the corresponding audio clip
            if (index < riddleAudioClips.Length && riddleAudioClips[index] != null)
            {
                audioSource.clip = riddleAudioClips[index];
                audioSource.Play();
            }
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
                finished = true;
            }
            gameWinLose.GetComponent<GameWinLose>().timeLeft = currentTime;
            gameWinLose.GetComponent<GameWinLose>().score = score;
            gameWinLose.SetActive(true);
            popupUIText.text = "";
        }
    }
}
