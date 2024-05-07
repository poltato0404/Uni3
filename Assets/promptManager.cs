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
    public void promptLaptop()
    {
        if(!isLaptopRetrieved)
        {
            Panel.SetActive(true);
            instructionTitle.text = "Item Found";
            instructionText.text = "You have found an old laptop. How about you open it in your inventory? ";
        }
    }
    public void promptCoin()
    {
        if(!isCoinRetrieved)
        {
            Panel.SetActive(true);
            instructionTitle.text = "Item Found";
            instructionText.text = "You have found a coin. It might be useful later on... ";
            isCoinRetrieved = true;
        }
    }
    public void promptDocument()
    {
        if(!isDocumentRetrieved)
        {
            Panel.SetActive(true);
            instructionTitle.text = "Item Found";
            instructionText.text = "You have Found a Document!";
            isDocumentRetrieved = true;
        }

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
