using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;




public class FileHandler 
{
    private string dataDirPath = "";
    private string dataFileName = "";


    public FileHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        string fullpath = Path.Combine(dataDirPath,dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullpath))
        {
           try 
        {
            string dataToLoad = "";
            using(FileStream stream = new FileStream(fullpath, FileMode.Open))
            {
                using(StreamReader reader = new StreamReader(stream))
                {
                    dataToLoad  = reader.ReadToEnd();
                }
            }

            loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
        }

        catch (Exception e)
        {
            Debug.LogError("Error while trying to load" + fullpath + "\n" + e);
        } 
        }
        return loadedData;

    }

    public void Save(GameData data)
    {
        string fullpath = Path.Combine(dataDirPath,dataFileName);
        try 
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullpath));
            string dataToString = JsonUtility.ToJson(data, true);
            using (FileStream stream = new FileStream(fullpath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToString);
                }
            }
        }

        catch (Exception e)
        {
            Debug.LogError("Error while trying to save" + fullpath + "\n" + e);
        }
    }
}
