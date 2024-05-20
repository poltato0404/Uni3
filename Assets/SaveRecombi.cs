using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveRecombi : MonoBehaviour, IDataPersistence
{
    public int score;
    public bool finished;
    public void SaveData(ref GameData data)
    {
        data.recombinantFinished = finished;
        data.recombinantScore = score;
        data.playerCoins += score / 10;
        data.coinsCollected += score / 10;
        Debug.Log("save recombinant dna");
    }

    public void LoadData(GameData data)
    {

    }
}
