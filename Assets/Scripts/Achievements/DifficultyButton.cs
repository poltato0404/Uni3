using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DifficultyButton : MonoBehaviour
{
    public Button easyButton;
    public Button averageButton;
    public Button hardButton;
    public TMP_Text minigameNameText;
    public TMP_Text cellTheoryScoreText;
    public TMP_Text cellStructureScoreText;
    public TMP_Text cellCycleScoreText;
    public TMP_Text plantOrganScoreText;
    public TMP_Text animalOrganScoreText;
    public TMP_Text feedBackScoreText;
    public TMP_Text mendelScoreText;
    public TMP_Text dogmaScoreText;
    public TMP_Text recombinantScoreText;

    private GameData loadedGameData;

    // Start is called before the first frame update
    void Start()
    {
        // Load game data
        loadedGameData = DataPersistenceManager.instance.LoadGame();

        // Display minigame data
        DisplayMinigameData();

        // Add click event listeners to buttons
        easyButton.onClick.AddListener(() => DisplayMinigameData());
        averageButton.onClick.AddListener(() => DisplayMinigameData());
        hardButton.onClick.AddListener(() => DisplayMinigameData());
    }

    // Method to display minigame data
    void DisplayMinigameData()
    {
        // Display minigame data in the UI
        minigameNameText.text = "Your Minigame Name";
        cellTheoryScoreText.text = "Cell Theory Score: " + loadedGameData.cellTheoryScore.ToString();
        cellStructureScoreText.text = "Cell Structure Score: " + loadedGameData.cellStructureScore.ToString();
        cellCycleScoreText.text = "Cell Cycle Score: " + loadedGameData.cellCycleScore.ToString();
        plantOrganScoreText.text = "Plant Organ Score: " + loadedGameData.plantOrganScore.ToString();
        animalOrganScoreText.text = "Animal Organ Score: " + loadedGameData.animalOrganScore.ToString();
        feedBackScoreText.text = "Feedback Score: " + loadedGameData.feedBackScore.ToString();
        mendelScoreText.text = "Mendel Score: " + loadedGameData.mendelScore.ToString();
        dogmaScoreText.text = "Dogma Score: " + loadedGameData.dogmaScore.ToString();
        recombinantScoreText.text = "Recombinant Score: " + loadedGameData.recombinantScore.ToString();
    }
}
