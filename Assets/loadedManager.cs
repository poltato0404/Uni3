using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadedManager : MonoBehaviour, IDataPersistence
{
    public void SaveData(ref GameData data){
        
        switch(data.currentLevel){
        case 1:
            data.loadedLevel1 = true;break;
        case 2:
        data.loadedLevel2 = true;break;
        case 3:
        data.loadedLevel3 = true;break;
        }
    
        }





    public void LoadData(GameData data){}


}
