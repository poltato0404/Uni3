using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveCT : MonoBehaviour, IDataPersistence
{
    public CardHolder card;
    public GameObject cardObj;
    public int score;
    public bool finished;
    public float time;

    public void SaveData(ref GameData data)
    {
        //cardObj.SetActive(true);
        data.cellTheoryFinished = finished;
        data.cellTheoryScore = score;
        data.cellTheoryTime = time;
        data.playerCoins += score / 10;
        data.coinsCollected += score / 10;
        Debug.Log("save cell theory");

    }
    public void LoadData(GameData data) { }
}
