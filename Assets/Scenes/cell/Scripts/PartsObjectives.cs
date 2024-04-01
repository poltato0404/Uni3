using UnityEngine;
using TMPro;

public class PartsObjectives : MonoBehaviour
{
    public static PartsObjectives instance;

    public GameObject gameWinLose;

    public int correctMatches = 0;
    

    public int score;
    public TextMeshProUGUI scoreText;

    [Header("Timer Properties")]
    public float totalTime;
    private float currentTime;
    [SerializeField] private TextMeshProUGUI timerText;

    private bool gameLost = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject); // Ensure only one instance exists
    }

    private void Start()
    {
        currentTime = totalTime;
    }

    private void Update()
    {
        if (!gameLost)
        {
            CountdownTimer();
            ScoreCounter();
            CheckWinCondition();
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
            // If the timer runs out, trigger game loss
            GameLost();
        }
    }

    private void CheckWinCondition()
    {
        if (correctMatches >= 6 && !gameWinLose.activeInHierarchy && !gameLost)
        {
            // Your existing win condition logic
            // For now, I'm leaving it unchanged
            if (score > 0)
            {
                //GameManager.Instance.isLevelComplete[levelId] = true;
            }

            gameWinLose.gameObject.GetComponent<GameWinLose>().score = score;

            gameWinLose.SetActive(true);
            Debug.Log("Game Win - You Win!");
        }
    }

    private void GameLost()
    {
        gameLost = true;
        // Implement game over logic here if needed
        Debug.Log("Game Over - You Lose!");
        gameWinLose.SetActive(true); // Activate game over screen
    }

    // Method to increment the score by the specified amount
    public void IncrementScore(int amount)
    {
        score += amount;
    }

    // Method to decrement the score by the specified amount
    public void DecrementScore(int amount)
    {
        score -= amount;
        if (score < 0)
            score = 0; // Ensure score doesn't go negative
    }

    
}
