using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveCT : MonoBehaviour, IDataPersistence
{
    public CardHolder card;
    public GameObject cardObj;
    public int score;
    public bool finished ;
     public void SaveData(ref GameData data)
    {
        cardObj.SetActive(true);
        data.cellTheoryFinished = finished;
        data.cellTheoryScore = score;
        Debug.Log("save cell theory");

    }
    public void LoadData(GameData data){}
}