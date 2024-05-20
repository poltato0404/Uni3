using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePS : MonoBehaviour, IDataPersistence
{
    public int score;
    public bool finished;
    public void SaveData(ref GameData data)
    {
        data.plantOrganFinished = finished;
        data.playerCoins += score / 10;
        data.coinsCollected += score / 10;
        data.plantOrganScore = score;
        Debug.Log("save plant organ systems");
    }

    public void LoadData(GameData data)
    {

    }
}
