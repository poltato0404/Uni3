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
            instructionText.text = "You've found Dr. Doe's laptop! This is key for accessing the minigames. \n After finding all 3 passwords, click the laptop in your inventory to use them when you're ready. ";
        }
    }
    public void promptCoin()
    {
        if (!isCoinRetrieved)
        {
            Panel.SetActive(true);
            instructionTitle.text = "Item Found";
            instructionText.text = "You found a coin! Collect these to upgrade your equipment. Keep exploring to find more. ";
            isCoinRetrieved = true;
        }
    }
    public void promptDocument(int y)
    {

        Panel.SetActive(true);
        instructionTitle.text = "Item Found";
        instructionText.text = "You've found a document with a password! \n " + evidenceStringList[y] + "\n Remember and collect all 3 to unlock the 3 minigames on the laptop. Keep searching. ";
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
