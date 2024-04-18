using UnityEngine;
using TMPro;

public class PlayerDataDisplay : MonoBehaviour, IDataPersistence
{
    public TMP_Text currentLevelText;
    public TMP_Text level1ScoreText;
    public TMP_Text level2ScoreText;
    public TMP_Text level3ScoreText;

    private GameData playerData;

    void Start()
    {
        // Load player data
        DataPersistenceManager.instance.LoadGame();

        UpdatePlayerDataUI();
    }

    private void UpdatePlayerDataUI()
    {
        currentLevelText.text = "Current Level: " + playerData.currentLevel;
        level1ScoreText.text = "Level 1 Score: " + playerData.level1Score;
        level2ScoreText.text = "Level 2 Score: " + playerData.level2Score;
        level3ScoreText.text = "Level 3 Score: " + playerData.level3Score;
    }

    // Implement the IDataPersistence interface methods

    public void LoadData(GameData data)
    {
        playerData = data;
        UpdatePlayerDataUI();
    }

    public void SaveData(ref GameData data)
    {
        // No need to save player data from this script
    }
}