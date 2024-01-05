using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameInstruction : MonoBehaviour
{
    public GameObject instructionsPanel;
    public TextMeshProUGUI instructionsText;
    public Button startButton;

    // Configurable parameters for different games
    public string gameInstructions;
    public string startMessage;

    void Start()
    {
        // Show instructions overlay when the game starts
        ShowInstructionsOverlay();

        // Attach the button click event listener
        startButton.onClick.AddListener(StartGame);
    }

    void ShowInstructionsOverlay()
    {
        // Enable the instructions panel
        instructionsPanel.SetActive(true);

        // Set and display the instructions text
        instructionsText.text = gameInstructions;

        // You can customize the appearance and positioning of the panel as needed
    }

    void StartGame()
    {
        // Disable the instructions panel
        instructionsPanel.SetActive(false);

        // Add your logic to start the actual game here
        Debug.Log(startMessage);
    }
}
