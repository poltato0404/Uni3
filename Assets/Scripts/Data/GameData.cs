using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData

{
    public string username;
    public string name;
    public string password;
    public string securityQuestion;
    public string securityAnswer;

    public int currentLevel;
    public int playerStamina;
    public int enemyCount;
    public List<Vector3> slotPosition;
    public List<int> slotReference;
    public Vector3 playerPos;
    public Vector3 devicePos;
    public int playerScore;
    public int playerCoins;
    public Vector3 guard1Pos;
    public Vector3 guard2Pos;
    public Vector3 guard3Pos;

    public bool loadedLevel1;
    public bool loadedLevel2;
    public bool loadedLevel3;
    public bool subtitle;
    public bool cellTheoryFinished;
    public bool cellStructureFinished;
    public bool cellCycleFinished;
    public bool plantOrganFinished;
    public bool animalOrganFinished;
    public bool feedBackFinished;
    public bool mendelFinished;
    public bool dogmaFinished;
    public bool recombinantFinished;

    public int level1Score;
    public int level2Score;
    public int level3Score;
    public int cellTheoryScore;
    public int cellStructureScore;
    public int cellCycleScore;
    public int plantOrganScore;
    public int animalOrganScore;
    public int feedBackScore;
    public int mendelScore;
    public int dogmaScore;
    public int recombinantScore;
    public int numberOfDrinks;
    public List<Vector3> coins;

    public List<InventoryItem> inventory;

    public GameData()
    {
        this.username = "";
        this.name = "";
        this.password = "";
        this.securityQuestion = "";
        this.securityAnswer = "";

        this.currentLevel = 1;
        this.playerStamina = 100;
        this.enemyCount = 0;
        this.playerCoins = 0;
        this.slotPosition = new List<Vector3>();
        this.slotReference = new List<int>();
        this.playerPos = new Vector3(0, 1.5f, 0);
        this.devicePos = new Vector3(0, 0, 0);
        this.coins = new List<Vector3>();
        this.playerScore = 0;
        this.guard1Pos = new Vector3(0, 1.5f, 0);
        this.guard2Pos = new Vector3(0, 1.5f, 0);
        this.guard3Pos = new Vector3(0, 1.5f, 0);
        this.loadedLevel1 = false;
        this.loadedLevel2 = false;
        this.loadedLevel3 = false;
        this.subtitle = true;
        this.cellTheoryFinished = false;
        this.cellStructureFinished = false;
        this.cellCycleFinished = false;
        this.plantOrganFinished = false;
        this.animalOrganFinished = false;
        this.feedBackFinished = false;
        this.mendelFinished = false;
        this.dogmaFinished = false;
        this.recombinantFinished = false;
        this.level1Score = 0;
        this.level2Score = 0;
        this.level3Score = 0;
        this.cellTheoryScore = 0;
        this.cellStructureScore = 0;
        this.cellCycleScore = 0;
        this.plantOrganScore = 0;
        this.animalOrganScore = 0;
        this.feedBackScore = 0;
        this.mendelScore = 0;
        this.dogmaScore = 0;
        this.recombinantScore = 0;


        this.inventory = new List<InventoryItem>();

    }

}
