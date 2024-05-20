using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDog : MonoBehaviour, IDataPersistence
{
    public int score;
    public bool finished;
    public void SaveData(ref GameData data)
    {
        data.animalOrganFinished = finished;
        data.animalOrganScore = score;
        data.playerCoins += score / 10;
        data.coinsCollected += score / 10;
        Debug.Log("save animal organ systems");
    }

    public void LoadData(GameData data)
    {

    }
}

