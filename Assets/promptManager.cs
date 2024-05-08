using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class promptManager : MonoBehaviour, IDataPersistence
{
    bool isLaptopRetrieved;
    bool isCoinRetrieved;
    bool isDocumentRetrieved;
    [SerializeField] private TextMeshProUGUI instructionText;
    [SerializeField] private TextMeshProUGUI instructionTitle;
    [SerializeField] private GameObject Panel;
    public List<string> evidenceStringList;
    public void promptLaptop()
    {
        if (!isLaptopRetrieved)
        {
            Panel.SetActive(true);
            instructionTitle.text = "Item Found";
            instructionText.text = "You have found an old laptop. It looks like it belong to Dr. Doe. \n You Remebered that he uses bits_blitz as an alias. How about you open it in your inventory? ";
        }
    }
    public void promptCoin()
    {
        if (!isCoinRetrieved)
        {
            Panel.SetActive(true);
            instructionTitle.text = "Item Found";
            instructionText.text = "You have found a coin. It might be useful later on... ";
            isCoinRetrieved = true;
        }
    }
    public void promptDocument(int y)
    {

        Panel.SetActive(true);
        instructionTitle.text = "Item Found";
        instructionText.text = "You have Found a Document \n Labeled " + evidenceStringList[y] + ". \n Try inputting it into a device";
        isDocumentRetrieved = true;


    }
    public void SaveData(ref GameData data)
    {
        data.isDocumentRetrieved = isDocumentRetrieved;
        data.isCoinRetrieved = isCoinRetrieved;


    }
    public void LoadData(GameData data)
    {
        isDocumentRetrieved = data.isDocumentRetrieved;
        isCoinRetrieved = data.isCoinRetrieved;
        isLaptopRetrieved = data.isLaptopRetrieved;
    }
}
