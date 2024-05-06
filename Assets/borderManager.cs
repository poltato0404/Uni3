using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class borderManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] GameObject easy, medium, hard;
    public void SaveData(ref GameData data)
    {

    }

    public void LoadData(GameData data)
    {
        switch (data.currentLevel)
        {
            case 1: easy.SetActive(true); break;
            case 2: medium.SetActive(true); break;
            case 3: hard.SetActive(true); break;
        }

    }
}
