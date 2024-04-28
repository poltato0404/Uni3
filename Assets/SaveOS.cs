using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveOS : MonoBehaviour, IDataPersistence
{
    public int score;
    public bool finished;
    public void SaveData(ref GameData data)
    {
        data.animalOrganFinished = finished;
        data.animalOrganScore = score;
        Debug.Log("save animal organ systems");
    }

    public void LoadData(GameData data)
    {

    }
}

