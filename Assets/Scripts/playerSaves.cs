using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerData
{
    public List<int> xPos;
    public List<int> zPos;

    public List<int> slot;

    public Vector3 playerCoord;
    public Vector3 minigame1Coord;
    public Vector3 minigame2Coord;
    public Vector3 minigame3Coord;
}

public class SaveLoadJSON : MonoBehaviour
{
    PlayerData playerData;
    string saveFilePath;

    void Start()
    {
        playerData = new PlayerData();
        playerData.playerCoord = new Vector3(0, 0, 0);

        saveFilePath = Application.persistentDataPath + "/PlayerData.json";
    }
    public void SaveGame()
    {
        string savePlayerData = JsonUtility.ToJson(playerData);
        File.WriteAllText(saveFilePath, savePlayerData);

        Debug.Log("Save file created at: " + saveFilePath);
    }

    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string loadPlayerData = File.ReadAllText(saveFilePath);
            playerData = JsonUtility.FromJson<PlayerData>(loadPlayerData);


        }
        else
            Debug.Log("There is no save files to load!");

    }

    public void DeleteSaveFile()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);

            Debug.Log("Save file deleted!");
        }
        else
            Debug.Log("There is nothing to delete!");
    }
}
