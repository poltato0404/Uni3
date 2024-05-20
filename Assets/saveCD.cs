using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveCD : MonoBehaviour, IDataPersistence
{

    public GameObject ibjec;
    public int score;
    public bool finished;
    public void SaveData(ref GameData data)
    {
        ibjec.SetActive(true);
        data.cellCycleFinished = finished;
        data.cellCycleScore = score;
        data.playerCoins += score / 10;
        data.coinsCollected += score / 10;
        Debug.Log("save cell theory");

    }
    public void LoadData(GameData data) { }
}
