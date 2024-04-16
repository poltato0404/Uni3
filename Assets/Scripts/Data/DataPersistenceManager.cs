using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    
    [Header("File Storage Config")]
    [SerializeField] private string filename;
    
    private GameData gameData;
    private List<IDataPersistence> dataPersistentObects;
    private FileHandler handler;
    public static DataPersistenceManager instance {get; private set;}

   private void Awake()
   {
        if (instance != null)
        {
            Debug.Log("There is more than one instance of Data persistence");
        }
        instance = this;
   }
  
    private void Start()
    {
        this.handler = new FileHandler(Application.persistentDataPath, filename);
        this.dataPersistentObects = FindAllDataPersistentObjects();
        LoadGame();
    }


   public void NewGame()
   {
        this.gameData = new GameData();
   }
   

   public void LoadGame()
   {

        this.gameData = handler.Load();
        //TODO load save
        if(null == this.gameData)
        {
            NewGame();
        }
        foreach (IDataPersistence dataPersistentObj in dataPersistentObects)
        {
            dataPersistentObj.LoadData(gameData);
            Debug.Log("load game");
        } 
   }

   public void SaveGame()
   {
        foreach (IDataPersistence dataPersistentObj in dataPersistentObects)
        {
            dataPersistentObj.SaveData(ref gameData);
        } 
        handler.Save(gameData);
   }

   private void OnApplicationQuit()
   {
    SaveGame();
   }
   private void OnDestroy()
   {
    SaveGame();
   }


   private List<IDataPersistence> FindAllDataPersistentObjects()
   {
    IEnumerable<IDataPersistence> dataPersistentObects = FindObjectsOfType<MonoBehaviour>()
    .OfType<IDataPersistence>();

    return new List<IDataPersistence>(dataPersistentObects); 
   }
}
