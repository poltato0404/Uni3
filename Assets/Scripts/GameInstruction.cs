using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameInstruction : MonoBehaviour
{
    public GameObject instructionsPanel;
    public TextMeshProUGUI instructionsText;
    public Button startButton;

    public TMP_Text scoreText; // Reference to TextMeshPro Text element for displaying score
    public TMP_Text timerText; // Reference to TextMeshPro Text element for displaying timer

    private bool isGameActive = false;
    private int score = 0;
    private float timer = 25f;

    void Start()
    {
        ShowInstructionsOverlay();
        startButton.onClick.AddListener(StartGame);
    }

    void ShowInstructionsOverlay()
    {
        instructionsPanel.SetActive(true);
        instructionsText.text = GetGameInstructions();
    }

    void StartGame()
    {
        instructionsPanel.SetActive(false);
        isGameActive = true;
    }

    void Update()
    {
        if (isGameActive)
        {
            if (timer > 0f)
            {
                timer -= Time.deltaTime;
                UpdateTimerUI();
            }
            else
            {
                EndGame(); // End the game when the timer reaches 0
            }
        }
    }

    void UpdateTimerUI()
    {
        timerText.text = "Time: " + Mathf.Ceil(timer).ToString();
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI(); // Update the score UI when score changes
    }

    public void EndGame()
    {
        isGameActive = false;
        // Add any logic to end the game here
    }

    protected virtual string GetGameInstructions()
    {
        return "Game instructions";
    }
}
