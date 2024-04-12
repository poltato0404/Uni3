using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 

{
    public int currentLevel;
    public int playerStamina;
    public int enemyCount;
    public List<Vector3> slotPosition;
    public List<int> slotReference;
    public Vector3 playerPos;
    public Vector3 devicePos;
    public int playerScore;
    public Vector3 guard1Pos; 
    public Vector3 guard2Pos; 
    public bool loadedLevel1;
    public bool loadedLevel2;
    public bool loadedLevel3;
    public bool subtitle;
    public bool cellTheoryFinished;
    public bool cellStructureFinished;
    public bool cellCycleFinished;
    public bool plantOrganFinished;
    public bool animalOrganFInished;
    public bool feedBackFinished;
    public bool mendelFinished;
    public bool dogmaFinished;
    public bool recombinantFinished;
  
    public GameData(){
        this.currentLevel = 1;
        this.playerStamina = 100;
        this.enemyCount = 0;
        slotPosition = new List<Vector3>();
        slotReference = new List<int>();
        this.playerPos = new Vector3(0,0,0);
        this.devicePos= new Vector3(0,0,0);
        this.playerScore = 0;
        this.guard1Pos = new Vector3(0,1.5f,0);
        this.guard2Pos= new Vector3(0,1.5f,0);
        this.loadedLevel1=false;
        this.loadedLevel2=false;
        this.loadedLevel3=false;
        this.subtitle = true;
        this.cellTheoryFinished = false;
        this.cellStructureFinished = false;
        this.cellCycleFinished = false;
        this.plantOrganFinished = false;
        this.animalOrganFInished = false;
        this.feedBackFinished = false;
        this.mendelFinished = false;
        this.dogmaFinished = false;
        this.recombinantFinished = false;


    }
   
}
