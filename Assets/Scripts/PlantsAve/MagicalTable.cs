using System.Collections;
using UnityEngine;

public class MagicalTable : MonoBehaviour
{
    public GameObject quizManagerObject; // Reference to the QuizManager object
    private QuizManager quizManager;

    private int collectedOrgans = 0;
    public int requiredOrgans = 5;

    // Start is called before the first frame update
    void Start()
    {
        quizManager = quizManagerObject.GetComponent<QuizManager>();
    }

    // OnTriggerEnter is called when the player enters the collider of the table
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CollectibleOrgan"))
        {
            CollectOrgan(other.gameObject);
        }
    }

    void CollectOrgan(GameObject organ)
    {
        // Disable the organ (you may want to play a collection animation here)
        organ.SetActive(false);

        // Increment the collected organs count
        collectedOrgans++;

        // Check if all required organs are collected
        if (collectedOrgans == requiredOrgans)
        {
            // Trigger the quiz when all organs are collected
            StartCoroutine(StartQuiz());
        }
    }

    IEnumerator StartQuiz()
    {
        // Wait for a moment before starting the quiz (you can add transition effects here)
        yield return new WaitForSeconds(1f);

        // Call a method to start the quiz in the QuizManager script
        quizManager.StartQuiz();
    }
}
