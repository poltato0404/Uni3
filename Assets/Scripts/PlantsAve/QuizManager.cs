using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public static QuizManager Instance;

    public GameObject quizPanel;
    public Text questionText;
    public Text answerFeedbackText;

    private int currentQuestionIndex = 0;

    // Example questions and answers (replace with your own)
    private string[] questions = { "What is the main function of plant roots?", "How do leaves contribute to photosynthesis?" };
    private string[][] answers = {
        new string[] { "Absorb water and nutrients", "Conduct photosynthesis", "Support the plant", "Store energy" },
        new string[] { "Absorb sunlight", "Store water", "Produce oxygen", "Conduct transpiration" }
    };

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        quizPanel.SetActive(false);
    }

    public void StartQuiz()
    {
        // Initialize the quiz
        currentQuestionIndex = 0;
        ShowQuizPanel();
        DisplayQuestion();
    }

    void ShowQuizPanel()
    {
        // Activate the quiz panel (you can add transition effects here)
        quizPanel.SetActive(true);
    }

    void DisplayQuestion()
    {
        // Display the current question
        questionText.text = questions[currentQuestionIndex];

        // Display answer options
        for (int i = 0; i < answers[currentQuestionIndex].Length; i++)
        {
            // Create buttons for each answer option (you can use Unity UI buttons for this)
            // Example: Instantiate a button prefab and set its text and onClick event
        }
    }

    public void CheckAnswer(string selectedAnswer)
    {
        // Check if the selected answer is correct (implement this based on your logic)
        bool isCorrect = IsAnswerCorrect(selectedAnswer);

        // Display feedback to the player
        if (isCorrect)
        {
            answerFeedbackText.text = "Correct!";
            // Increment the question index for the next question
            currentQuestionIndex++;
            // Check if all questions are answered
            if (currentQuestionIndex < questions.Length)
            {
                // Display the next question
                StartCoroutine(DisplayNextQuestion());
            }
            else
            {
                // Quiz completed
                StartCoroutine(QuizCompleted());
            }
        }
        else
        {
            answerFeedbackText.text = "Incorrect. Try again!";
        }
    }

    IEnumerator DisplayNextQuestion()
    {
        // Wait for a moment before displaying the next question (you can add transition effects here)
        yield return new WaitForSeconds(1f);
        DisplayQuestion();
    }

    IEnumerator QuizCompleted()
    {
        // Wait for a moment before completing the quiz (you can add transition effects here)
        yield return new WaitForSeconds(1f);

        // Deactivate the quiz panel and provide additional feedback to the player
        quizPanel.SetActive(false);
        Debug.Log("Quiz completed!");
    }

    bool IsAnswerCorrect(string selectedAnswer)
    {
        // Implement your logic to check if the selected answer is correct
        // Compare the selectedAnswer with the correct answer for the current question
        // Return true if correct, false otherwise
        return true; // Replace this with your actual logic
    }
}
