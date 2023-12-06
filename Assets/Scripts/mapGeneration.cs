using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class mapGeneration : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject floor;
    //Instantiate(floor, new Vector3(0, 0, 0), Quaternion.identity);
    [SerializeField] private int xLength, zLength;
    [SerializeField] private GameObject slot1, slot2, slot3, slot4, slot5, slot6, slot7,slot8, slot9, slot10, slot11;
    List<GameObject> slotList;
    private int[] fromDownValues;
    private int[] fromRightValues;
    private int[] fromUpValues;
    private int[] fromLeftValues;
    private List<int> pathX; 
    private List<int> pathZ;
    private List<int> pathDirection;
    private List<int> possibleDirection;
    private int currentDirection;
    private int currentPositionX;
    private int currentPositionZ;
    private int nextPositionX;

    private int nextPositionZ;

    private int Lslot;
    private int Dslot;
    private int Rslot;
    private int Uslot;
    private int GDirection;
    private int currentSlot;
    private int maxPositionX;
    private int maxPositionZ;
    //1=up,2=right,3=down,4=left
    void Start()
    {       
        //initialize lists and arrays
        pathX = new List<int>();
        pathZ = new List<int>();
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
        currentPositionZ = 0;
        currentPositionX = 0;
        mapSize = (xLength * zLength)-1;
        generateSlot(fromDown(),currentPositionX,currentPositionZ);
        currentPath(currentPositionX, currentPositionZ);
        Debug.Log("direction : "+pathDirection[0]);
        pathDirection.Add(getDirection(currentSlot));
        while (mapSize != 0){        
            if(pathDirection[(pathDirection.Count-1)] == 1){
                changePos();
                generateSlot(fromDown(),nextPositionX,nextPositionZ); 
                Debug.Log(nextPositionX+","+nextPositionZ);
                currentPath(nextPositionX, nextPositionZ);
            }      
            if(pathDirection[(pathDirection.Count-1)] == 2){
                changePos();
                generateSlot(fromLeft(),nextPositionX,nextPositionZ); 
                Debug.Log(nextPositionX+","+nextPositionZ);
                currentPath(nextPositionX, nextPositionZ);
            }  
            if(pathDirection[(pathDirection.Count-1)] == 3){
                changePos();
                generateSlot(fromUp(),nextPositionX,nextPositionZ); 
                Debug.Log(nextPositionX+","+nextPositionZ);
                currentPath(nextPositionX, nextPositionZ);
            }  
            if(pathDirection[(pathDirection.Count-1)] == 4){
                changePos();
                generateSlot(fromRight(),nextPositionX,nextPositionZ); 
                Debug.Log(nextPositionX+","+nextPositionZ);
                currentPath(nextPositionX, nextPositionZ);
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



    

    void generateSlot(int slot, int xCoordinate, int zCoordinate){
        
        Instantiate(slotReference(slot-1),new Vector3(xCoordinate, 0, zCoordinate), Quaternion.identity, transform);
        Debug.Log(slotReference(slot-1));
    }

    GameObject slotReference(int slotPosition){
        return(slotList[(slotPosition)]);
    }

    int fromDown(){
        currentDirection = 1;
        pathDirection.Add(1);      
        Dslot = fromDownValues[Random.Range(0,6)];
        currentSlot = Dslot;
        Debug.Log("fromdown"+currentSlot);
        return(Dslot);    
        

    }
    int fromRight(){
        currentDirection = 4;
        pathDirection.Add(4);
        Rslot = fromRightValues[Random.Range(0,6)];
        currentSlot = Rslot;
        Debug.Log("fromRight:"+currentSlot);
        return(Rslot);   

    }
    int fromUp(){
        currentDirection = 3;
        pathDirection.Add(3);
        Uslot = fromUpValues[Random.Range(0,6)];
        currentSlot = Uslot;
        Debug.Log("fromup:"+currentSlot);
        return(Uslot);    

    }
    int fromLeft(){
        currentDirection = 2;
        pathDirection.Add(2);
        Lslot = fromLeftValues[Random.Range(0,6)];
        currentSlot = Lslot;
        Debug.Log("fromLeft:"+currentSlot);
        return(Lslot);  

    }

    

    void currentPath( int x, int z){      
        pathX.Add(x);
        pathZ.Add(z);
    }

    int getDirection(int feedSlot){
        maxPositionX = (xLength/2*5);
        maxPositionZ = (zLength*5);
        possibleDirection = new List<int>();
        possibleDirection.Clear();
        possibleDirection.Add(1);
        possibleDirection.Add(2);
        possibleDirection.Add(3);
        possibleDirection.Add(4);
        Debug.Log("directions initial" + possibleDirection.Count);

        switch(feedSlot){
            case 1: 
                break;
            case 2: 
                possibleDirection.Remove(1);
                possibleDirection.Remove(4);
                Debug.Log(feedSlot+"remove 1 and 4");
                break;
            case 3: 
                possibleDirection.Remove(1);
                possibleDirection.Remove(2);
                Debug.Log(feedSlot+"remove 1 and 2");
                break;
            case 4: 
                possibleDirection.Remove(2);
                possibleDirection.Remove(4);
                Debug.Log(feedSlot+"remove 2 and 4");
                break;
            case 5: 
                possibleDirection.Remove(1);
                possibleDirection.Remove(3);
                Debug.Log(feedSlot+"remove 1 and 3");
                break;
            case 6: 
                possibleDirection.Remove(2);
                possibleDirection.Remove(3);
                Debug.Log(feedSlot+"remove 2 and 3");
                break;
            case 7: 
                possibleDirection.Remove(3);
                possibleDirection.Remove(4);
                Debug.Log(feedSlot+"remove 3 and 4");
                break;
            case 8: 
                possibleDirection.Remove(3);
                Debug.Log(feedSlot+"remove 3");
                break;
            case 9: 
                possibleDirection.Remove(4);
                Debug.Log(feedSlot+"remove 4");
                break;
            case 10: 
                possibleDirection.Remove(1);
                Debug.Log(feedSlot+"remove 1");
                break;
            case 11: 
                possibleDirection.Remove(2);
                Debug.Log(feedSlot+"remove 2");
                break;
        }
        Debug.Log("directions after switch" + possibleDirection.Count);


        if(nextPositionX == maxPositionX){
            possibleDirection.Remove(2);
            Debug.Log("max X remove to right");
        }
        if(nextPositionX == (maxPositionX*-1)){
            possibleDirection.Remove(4);
            Debug.Log("max X remove left");
        }
        if(nextPositionZ == (0)){
            possibleDirection.Remove(3);
             Debug.Log("0 yz so remove down");
        }
        if(nextPositionZ == (maxPositionZ)){
            possibleDirection.Remove(1);
             Debug.Log("max z remove up");
        }

        Debug.Log("directions after edge constraints" + possibleDirection.Count);
        if(checkIsNotAvailable((nextPositionX - 5),nextPositionZ)){
            possibleDirection.Remove(4);
            Debug.Log("left is  occupied");

        }
        if(checkIsNotAvailable((nextPositionX + 5),nextPositionZ)){
            possibleDirection.Remove(2);
            Debug.Log("right is occupied");
        }
        if(checkIsNotAvailable(nextPositionX,(nextPositionZ + 5))){
            possibleDirection.Remove(1);
            Debug.Log("up is  occupied");
        }
        if(checkIsNotAvailable(nextPositionX,(nextPositionZ - 5))){
            possibleDirection.Remove(3);
            Debug.Log("down is  occupied");
        }
        

        Debug.Log("possibleDirection after checking" + possibleDirection.Count);
        GDirection = possibleDirection[Random.Range(0, possibleDirection.Count-1)];
        Debug.Log("diection"+GDirection);
        return(GDirection);

        

    }

    void changePos(){
        switch(GDirection){
            case 1:
                nextPositionZ = nextPositionZ + 5;
                break;       
            case 2:
                nextPositionX = nextPositionX + 5;
                break;
            case 3:
                nextPositionZ = nextPositionZ - 5;
                break;
            case 4:
                nextPositionX = nextPositionX - 5;
                break;
        }
    }



    bool checkIsNotAvailable(int checkX, int checkZ){
        for (int i = 0; i < pathX.Count; i++)
        {
            if (pathX[i] == checkX && pathZ[i] == checkZ)
            {
                return true;
            }
        }

        return false;
    }      
    

    



    }

    

    

    

    

