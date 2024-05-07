using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class resetData : MonoBehaviour
{
    private string dataDirPath = "";
    private string dataFileName = "";


    public resetData(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public void ResetData()
    {
        dataDirPath = Application.persistentDataPath; 
        dataFileName = "data.json"; 
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        if (File.Exists(fullPath))
        {
            try
            {
                File.Delete(fullPath);
                Debug.Log("Game data reset by deleting " + fullPath);
            }
            catch (Exception e)
            {
                Debug.LogError("Error while trying to reset " + fullPath + "\n" + e);
            }
        }
        else
        {
            Debug.LogWarning("Game data reset attempted, but file does not exist at " + fullPath);
        }
    }

}
