using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plateScript : MonoBehaviour
{
    [SerializeField] chickenManager chkMng;
    [SerializeField] private int chickenCount;
    
    void Start(){
        chickenCount = 0;
    }
    
    void breedChicken(){}
    
    
    public void addChicken(){
        if(1 == chickenCount){
        chkMng.resetChickens();
        breedChicken();
        chickenCount = 0;
        }
        else {
            chickenCount +=1;
        }


    }

}
