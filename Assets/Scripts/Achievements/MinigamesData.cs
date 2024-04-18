using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MinigamesData : MonoBehaviour, IDataPersistence
{
    public Button easyButton;
    public Button aveButton;
    public Button hardButton;
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
        DataPersistenceManager.instance.LoadGame();

        // Display minigame data
        DisplayMinigameData();

        // Add click event listeners to buttons
        easyButton.onClick.AddListener(() => DisplayMinigameData());
        aveButton.onClick.AddListener(() => DisplayMinigameData());
        hardButton.onClick.AddListener(() => DisplayMinigameData());
    }

    // Method to display minigame data
    void DisplayMinigameData()
    {
        // Display minigame data in the UI
        cellTheoryScoreText.text = "Best Score: " + loadedGameData.cellTheoryScore.ToString();
        cellStructureScoreText.text = "Best Score: " + loadedGameData.cellStructureScore.ToString();
        cellCycleScoreText.text = "Best Score: " + loadedGameData.cellCycleScore.ToString();
        plantOrganScoreText.text = "Best Score: " + loadedGameData.plantOrganScore.ToString();
        animalOrganScoreText.text = "Best Score: " + loadedGameData.animalOrganScore.ToString();
        feedBackScoreText.text = "Best Score: " + loadedGameData.feedBackScore.ToString();
        mendelScoreText.text = "Best Score: " + loadedGameData.mendelScore.ToString();
        dogmaScoreText.text = "Best Score: " + loadedGameData.dogmaScore.ToString();
        recombinantScoreText.text = "Best Score: " + loadedGameData.recombinantScore.ToString();
    }

    public void LoadData(GameData data)
    {
        loadedGameData = data;
        DisplayMinigameData();
    }
    public void SaveData(ref GameData data)
    {
        
    }
}
