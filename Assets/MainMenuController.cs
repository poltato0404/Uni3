using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MainMenuController : MonoBehaviour
{
    public Button continueButton;

    void OnEnable()
    {
        // Update the interactable property of the continue button when the main menu is displayed
        UpdateContinueButtonInteractability();
    }

    void UpdateContinueButtonInteractability()
    {
        // Check if the file exists
        bool hasSavedData = File.Exists(Application.persistentDataPath + "/data.json");

        // Set the interactable property of the continue button
        continueButton.interactable = hasSavedData;
    }

    // Other menu functionality like starting a new game, accessing settings, etc.
}
