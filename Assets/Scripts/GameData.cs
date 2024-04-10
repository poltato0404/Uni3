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

    public GameData(){
        this.currentLevel = 1;
        this.playerStamina = 100;
        this.enemyCount = 0;
        slotPosition = new List<Vector3>();
        slotReference = new List<int>();
        this.playerPos = new Vector3(0,0,0);
        this.devicePos= new Vector3(0,0,0);
        this.playerScore = 0;
        this.guard1Pos = new Vector3(0,0,0);
        this.guard2Pos= new Vector3(0,0,0);
        this.loadedLevel1=false;
        this.loadedLevel2=false;
        this.loadedLevel3=false;

    }
   
}
