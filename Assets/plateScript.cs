using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plateScript : MonoBehaviour
{
    [SerializeField] chickenManager chkMng;
    [SerializeField] private int chickenCount;
    [SerializeField] resultPanel results;
    [SerializeField] GameObject resultGameObject;
    ChickenTraits chicken1, chicken2;
    void Start(){
        chickenCount = 0;
    }
    
    void assignChicken1(ChickenTraits chick)
    {
        chicken1 = chick;
    }
    void assignChicken2(ChickenTraits chick)
    {
        chicken2 = chick;
    }
    
    
    public void addChicken(ChickenTraits chick){
        if(1 == chickenCount){
        assignChicken2(chick);
        chkMng.resetChickens();
        resultGameObject.SetActive(true);
        results.displayResult(chicken1, chicken2);
        chickenCount = 0;
        }
        else {
            assignChicken1(chick);
            chickenCount +=1;
        }
    }

}
