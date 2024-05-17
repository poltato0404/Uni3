using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class objectivesManager : MonoBehaviour, IDataPersistence
{
    public int remainingCoins;
    public int remainingDocuments;
    public bool isLaptopRetrieved;
    public int currentLeve;
    public mapLoader mapL;

    public TextMeshProUGUI levelText;

    public TextMeshProUGUI Main1;
    public TextMeshProUGUI Main2;
    public TextMeshProUGUI Main3;

    public TextMeshProUGUI minor1;
    public TextMeshProUGUI minor2;
    public TextMeshProUGUI minor3;
    public int stringCountEvidence;
    public PlayerBehaviour playerBehaviour;



    public void SaveData(ref GameData data)
    {


    }

    public void LoadData(GameData data)
    {
        currentLeve = data.currentLevel;
        levelText.text = "Level " + currentLeve;
        Main2.text = "- Find all the documents in the level";
        Main3.text = "- Find all the coins in the level (Optional)";

    }

    void Update()
    {

        isLaptopRetrieved = mapL.gotLaptop;
        if (isLaptopRetrieved)
        {
            minor1.text = "<s>(1/1)</s>";
            Main1.text = "<s>- Find a Laptop that belongs to Dr. Doe</s>";
        }

        else
        {
            minor1.text = "(0/1)";
            Main1.text = "- Find a Laptop that belongs to Dr. Doe";
        }


        minor2.text = "(" + playerBehaviour.stringEvidenceCount + "/3)";
        if (playerBehaviour.stringEvidenceCount == 3)
        {
            Main2.text = "<s>- Find all the documents in the level</s>";
            minor2.text = "(3/3)";
        }

        minor3.text = "(" + playerBehaviour.coinsCollected + "/3)";


    }
}
