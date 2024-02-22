using System.Collections;
using UnityEngine;

public class PlantOrganCollector : MonoBehaviour
{
    private int collectedOrgans = 0;
    public int requiredOrgans = 5;

    // Update is called once per frame
    void Update()
    {
        // Check for interaction input (e.g., press 'E' to collect)
        if (Input.GetKeyDown(KeyCode.E))
        {
            CollectOrgan();
        }
    }

    void CollectOrgan()
    {
        // Perform raycasting to check if the player is near a collectible organ
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject organ = hit.collider.gameObject;

            // Check if the hit object is a collectible organ
            if (organ.CompareTag("CollectibleOrgan"))
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
        }
    }

    IEnumerator StartQuiz()
    {
        // Wait for a moment before starting the quiz (you can add transition effects here)
        yield return new WaitForSeconds(1f);

        // Call a method to start the quiz (implement this method in your QuizManager script)
        QuizManager.Instance.StartQuiz();
    }
}

