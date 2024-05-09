using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class objectivesManager : MonoBehaviour, IDataPersistence
{
    public int remainingCoins;
    public int remainingDocuments;
    public bool isLaptopRetrieved;
    public int currentLevel;
    public mapLoader mapL;

    public TextMeshProUGUI levelText;

    public TextMeshProUGUI Main1;
    public TextMeshProUGUI Main2;
    public TextMeshProUGUI Main3;

    public TextMeshProUGUI minor1;
    public TextMeshProUGUI minor2;
    public TextMeshProUGUI minor3;

    public void SaveData(ref GameData data)
    {
        

    }

    public void LoadData(GameData data)
    {

    }

    void Update()
    {
       isLaptopRetrieved = mapL.gotLaptop; 

    }
}
