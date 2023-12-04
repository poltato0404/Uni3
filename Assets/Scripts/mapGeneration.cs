using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class mapGeneration : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject floor;
    //Instantiate(floor, new Vector3(0, 0, 0), Quaternion.identity);
    [SerializeField] private int xLength, yLength;
    [SerializeField] private GameObject slot1, slot2, slot3, slot4, slot5, slot6, slot7,slot8, slot9, slot10, slot11;
    List<GameObject> slotList;
    private int[] fromDownValues;
    private int[] fromRightValues;
    private int[] fromUpValues;
    private int[] fromLeftValues;
    private List<int> pathX = new List<int>();
    private List<int> pathY = new List<int>();

    void Start()
    {       
        //initialize lists and arrays
        slotList = new List<GameObject>{slot1, slot2, slot3, slot4, slot5, slot6, slot7, slot8, slot9, slot10, slot11};
        fromDownValues = new int[]{1, 2, 3, 4, 9, 10, 11};
        fromRightValues = new int[]{1, 2, 5, 7, 8, 9, 10};
        fromUpValues = new int[]{1, 4, 6, 7, 8, 9, 11};
        fromLeftValues = new int[]{1, 3, 5, 6, 8, 10, 11};
        

        
        StartCoroutine(generateMaze());
    }

    IEnumerator generateMaze(){
        generateSlot(fromDown(),0,0);

        yield return null;
       
    }



    

    void generateSlot(int slot, int xCoordinate, int yCoordinate){
        
        Instantiate(slotReference(slot-1),new Vector3(xCoordinate, 0, yCoordinate), Quaternion.identity, transform);
        
    }

    GameObject slotReference(int slotPosition){
        return(slotList[(slotPosition)]);
    }

    int fromDown(){
        return(fromDownValues[Random.Range(0,6)]);  

    }
    int fromRight(){
        return(fromRightValues[Random.Range(0,6)]);  

    }
    int fromUp(){
        return(fromUpValues[Random.Range(0,6)]);  

    }
    int fromLeft(){
        return(fromLeftValues[Random.Range(0,6)]);  

    }

    //bool checkAvailable(int checkX, checkY){
        


    //}

    void currentPath(int x, int y){
        pathX.Add(x);
        pathY.Add(y);
    }

    void getDirection(){

    }

    

    

}