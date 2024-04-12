using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class minigameHandler : MonoBehaviour, IDataPersistence
{
    [SerializeField]GameObject  easy,average,hard;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveData(ref GameData data)
    {

    }
     public void LoadData( GameData data)
    {
        if(1 == data.currentLevel){easy.SetActive(true);}
        if(2 == data.currentLevel){average.SetActive(true);}
        if(3 == data.currentLevel){hard.SetActive(true);}
    }
    public void backToMaze()
    {
        SceneManager.LoadScene("level1");
    }
    public void goToCellCycle()
    {
        SceneManager.LoadScene("06CellMemory");
    }
    public void goToCellStructure()
    {
        SceneManager.LoadScene("CellStructure");
    }
    public void goToCellTheory()
    {
        SceneManager.LoadScene("04CellDivision");
    }
}
