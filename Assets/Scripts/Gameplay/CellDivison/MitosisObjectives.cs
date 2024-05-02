using GameEssentials.GameManager;
using TMPro;
using UnityEngine;

public class MitosisObjectives : MonoBehaviour
{
    // 4th minigame
    public int levelId = 3;

    public GameObject gameWinLose;

    public int correctMatches = 0;

    public int score;
    public TextMeshProUGUI scoreText;

    [Header("Timer Properties")]
    public float totalTime;
    private float currentTime;
    [SerializeField] private TextMeshProUGUI timerText;

    public static MitosisObjectives instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    private void Start()
    {
        currentTime = totalTime;
    }


    private void Update()
    {
        CountdownTimer();
        ScoreCounter();

        if (correctMatches >= 8 )
        {
            gameWinLose.SetActive(true);
            if (score > 0)
            {
                //dsadsaomdasjdmas
            }

            gameWinLose.GetComponent<GameWinLose>().timeLeft = currentTime;
            gameWinLose.GetComponent<GameWinLose>().score = score;
            
        }
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
            //Lose
            gameWinLose.GetComponent<GameWinLose>().timeLeft = currentTime;
            gameWinLose.GetComponent<GameWinLose>().score = score;
            gameWinLose.SetActive(true);
            //Debug.Log("Timer Ran out!");
        }
    }
}