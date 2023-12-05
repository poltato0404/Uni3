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
    private List<int> pathX; 
    private List<int> pathY;
    private List<int> pathDirection;
    private List<int> possibleDirection;
    private int currentDirection;
    private int currentPositionX;
    private int currentPositionY;
    private int nextPositionX;

    private int nextPositionY;

    private int Lslot;
    private int Dslot;
    private int Rslot;
    private int Uslot;
    private int GDirection;
    private int currentSlot;
    private int maxPositionX;
    private int maxPositionY;
    //1=up,2=right,3=down,4=left
    void Start()
    {       
        //initialize lists and arrays
        pathX = new List<int>();
        pathY = new List<int>();
        pathDirection = new List<int>();
        slotList = new List<GameObject>{slot1, slot2, slot3, slot4, slot5, slot6, slot7, slot8, slot9, slot10, slot11};
        fromDownValues = new int[]{1, 2, 3, 4, 9, 10, 11};
        fromRightValues = new int[]{1, 2, 5, 7, 8, 9, 10};
        fromUpValues = new int[]{1, 4, 6, 7, 8, 9, 11};
        fromLeftValues = new int[]{1, 3, 5, 6, 8, 10, 11};
        

        
        StartCoroutine(generateMaze());
    }

    IEnumerator generateMaze(){
        int mapSize; 
        currentPositionY = 0;
        currentPositionX = 0;
        mapSize = (xLength * yLength)-1;
        generateSlot(fromDown(),currentPositionX,currentPositionY);
        currentPath(currentPositionX, currentPositionY);
        Debug.Log("direction : "+pathDirection[0]);
        pathDirection.Add(getDirection(currentSlot));
        while (mapSize != 0){        
            if(pathDirection[(pathDirection.Count-1)] == 1){
                generateSlot(fromDown(),nextPositionX,nextPositionY); 
                changePos();
                currentPath(nextPositionX, nextPositionY);
            }      
            if(pathDirection[(pathDirection.Count-1)] == 2){
                generateSlot(fromLeft(),nextPositionX,nextPositionY); 
                changePos();
                currentPath(nextPositionX, nextPositionY);
            }  
            if(pathDirection[(pathDirection.Count-1)] == 3){
                generateSlot(fromUp(),nextPositionX,nextPositionY); 
                changePos();
                currentPath(nextPositionX, nextPositionY);
            }  
            if(pathDirection[(pathDirection.Count-1)] == 4){
                generateSlot(fromRight(),nextPositionX,nextPositionY); 
                changePos();
                currentPath(nextPositionX, nextPositionY);
            }       
            pathDirection.Add(getDirection(currentSlot));
            mapSize--;
        }
        //while(!mapSize == 0){
        //getdirection
        //generateslot
        //addtopath
        //mapSize--
        //}

        yield return null; 
    }



    

    void generateSlot(int slot, int xCoordinate, int yCoordinate){
        
        Instantiate(slotReference(slot-1),new Vector3(xCoordinate, 0, yCoordinate), Quaternion.identity, transform);
        Debug.Log(slotReference(slot-1));
    }

    GameObject slotReference(int slotPosition){
        return(slotList[(slotPosition)]);
    }

    int fromDown(){
        currentDirection = 1;
        pathDirection.Add(1);
        //while(){
        Dslot = fromDownValues[Random.Range(0,6)];
        //}
        currentSlot = Dslot;
        Debug.Log("fromdown"+currentSlot);
        return(Dslot);    
        

    }
    int fromRight(){
        currentDirection = 4;
        pathDirection.Add(4);
        Rslot = fromRightValues[Random.Range(0,6)];
        currentSlot = Rslot;
        return(Rslot);   

    }
    int fromUp(){
        currentDirection = 3;
        pathDirection.Add(3);
        Uslot = fromUpValues[Random.Range(0,6)];
        currentSlot = Uslot;
        return(Uslot);    

    }
    int fromLeft(){
        
        currentDirection = 2;
        pathDirection.Add(2);
        Lslot = fromLeftValues[Random.Range(0,6)];
        currentSlot = Lslot;
        return(Lslot);  

    }

    

    void currentPath( int x, int y){      
        pathX.Add(x);
        pathY.Add(y);
    }

    int getDirection(int feedSlot){
        maxPositionX = (xLength/2);
        maxPositionY = (yLength);
        possibleDirection = new List<int>();
        possibleDirection.Add(1);
        possibleDirection.Add(2);
        possibleDirection.Add(3);
        possibleDirection.Add(4);
         Debug.Log("Log1");
        switch(feedSlot){
            case 1: 
                break;
            case 2: 
                possibleDirection.Remove(1);
                possibleDirection.Remove(4);
                break;
            case 3: 
                possibleDirection.Remove(1);
                possibleDirection.Remove(2);
                break;
            case 4: 
                possibleDirection.Remove(2);
                possibleDirection.Remove(4);
                break;
            case 5: 
                possibleDirection.Remove(1);
                possibleDirection.Remove(3);
                break;
            case 6: 
                possibleDirection.Remove(2);
                possibleDirection.Remove(3);
                break;
            case 7: 
                possibleDirection.Remove(3);
                possibleDirection.Remove(4);
                break;
            case 8: 
                possibleDirection.Remove(3);
                break;
            case 9: 
                possibleDirection.Remove(4);
                break;
            case 10: 
                possibleDirection.Remove(1);
                break;
            case 11: 
                possibleDirection.Remove(2);
                break;
        }

        if(currentPositionX == maxPositionX){
            possibleDirection.Remove(2);
        }
        if(currentPositionX == (maxPositionX*-1)){
            possibleDirection.Remove(4);
        }
        if(currentPositionY == (0)){
            possibleDirection.Remove(3);
        }
        if(currentPositionY == (maxPositionY)){
            possibleDirection.Remove(1);
        }
        if(checkIsNotAvailable((nextPositionX - 5),nextPositionY)){
            possibleDirection.Remove(4);
        }
        if(checkIsNotAvailable((nextPositionX + 5),nextPositionY)){
            possibleDirection.Remove(2);
        }
        if(checkIsNotAvailable(nextPositionX,(nextPositionY + 5))){
            possibleDirection.Remove(1);
        }
        if(checkIsNotAvailable(nextPositionX,(nextPositionY - 5))){
            possibleDirection.Remove(3);
        }
        
        


        Debug.Log("possibleDirection.Count" + possibleDirection.Count);
        GDirection = possibleDirection[Random.Range(0, possibleDirection.Count-1)];
        Debug.Log("diection"+GDirection);

        
        Debug.Log("X:"+nextPositionX);
        Debug.Log("Y:"+nextPositionY);
        return(GDirection);

        

    }

    void changePos(){
        switch(GDirection){
            case 1:
                nextPositionY = nextPositionY + 5;
                break;       
            case 2:
                nextPositionX = nextPositionX + 5;
                break;
            case 3:
                nextPositionY = nextPositionY - 5;
                break;
            case 4:
                nextPositionX = nextPositionX - 5;
                break;
        }
    }



    bool checkIsNotAvailable(int checkX, int checkY){
        for (int i = 0; i < pathX.Count; i++)
        {
            if (pathX[i] == checkX && pathY[i] == checkY)
            {
                return true;
            }
        }

        return false;
    }      

    



    }

    

    

    

    

