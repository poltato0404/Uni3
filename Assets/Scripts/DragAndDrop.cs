using System.Collections.Generic;
using UnityEngine;

public class DragAndDropGame : MonoBehaviour
{
    public List<DraggableItem> draggableItems;
    public List<GameObject> targetObjects;

    private bool isGameActive = false;
    public List<DraggableItem> correctMatches = new List<DraggableItem>();

    void Start()
    {
        for (int i = 0; i < draggableItems.Count; i++)
        {
            if (i < targetObjects.Count)
            {
                draggableItems[i].Initialize(this, targetObjects[i]);
            }
            else
            {
                Debug.LogError("Not enough target objects for draggable items!");
            }
        }
    }

    void Update()
    {
        // You can add any game logic here that doesn't involve time, score, or buttons
    }

    public void StartGame()
    {
        isGameActive = true;
    }

    public void CorrectMatch(DraggableItem matchedItem)
    {
        // Handle correct match logic here
        // For example, you might disable the draggable item or play a sound
        // matchedItem.gameObject.SetActive(false);

        // Add the matched item to the list of correct matches
        correctMatches.Add(matchedItem);

        // Check if all items are matched
        if (correctMatches.Count == draggableItems.Count)
        {
            // Game over, all items matched
            Debug.Log("Game Over - All items matched!");
        }
    }
}
