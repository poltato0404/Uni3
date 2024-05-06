using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class mapGeneration : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject floor;
    //Instantiate(floor, new Vector3(0, 0, 0), Quaternion.identity);
    [SerializeField] public int xLength, zLength;
    [SerializeField] private GameObject slot1, slot2, slot3, slot4, slot5, slot6, slot7, slot8, slot9, slot10, slot11;
    List<GameObject> slotList;
    private int slotToSwap;
    private int replacementSlot;
    private int[] fromDownValues;
    private int[] fromRightValues;
    private int[] fromUpValues;
    private int[] fromLeftValues;
    public List<int> pathX;
    public List<int> pathZ;
    public List<int> toPassX;
    public List<int> toPassZ;
    public List<int> slotInMaze;
    public List<int> toPassSlot;
    private int itlog, gridX, gridZ;
    private List<int> pathDirection;
    private List<int> possibleDirection;
    public List<int> RemainingX;
    public List<int> RemainingZ;
    private List<int> newPathX;
    private List<int> newPathZ;
    private List<int> nextDirection;
    public int removed;

    private int currentPositionX;
    private int currentPositionZ;
    private int nextPositionX;
    private int nextPositionZ;
    private bool repositioned = false;
    private int loop;
    private int Lslot;
    private int Dslot;
    private int Rslot;
    private int Uslot;
    private int GDirection;
    private int currentSlot;
    private int maxPositionX;
    private int maxPositionZ;
    private int gridStartX;
    private int gridStartZ;
    [SerializeField] private int mapSize, emptySlot;
    private int origMap;
    private List<GameObject> instantiatedClones;
    [SerializeField] private GameObject scatter1;

    private Spawner parentScript;
    bool clearToPass = false;

    //1=up,2=right,3=down,4=left
    public void startGenerating()
    {
        //initialize lists and arrays

        pathX = new List<int>();
        pathZ = new List<int>();
        slotInMaze = new List<int>();
        newPathX = new List<int>();
        newPathZ = new List<int>();
        toPassX = new List<int>();
        toPassZ = new List<int>();
        toPassSlot = new List<int>();
        RemainingX = new List<int>();
        RemainingZ = new List<int>();
        nextDirection = new List<int>();
        pathDirection = new List<int>();
        instantiatedClones = new List<GameObject>();
        slotList = new List<GameObject> { slot1, slot2, slot3, slot4, slot5, slot6, slot7, slot8, slot9, slot10, slot11 };
        fromDownValues = new int[] { 1, 2, 3, 4, 9, 10, 11 };
        fromRightValues = new int[] { 1, 2, 5, 7, 8, 9, 10 };
        fromUpValues = new int[] { 1, 4, 6, 7, 8, 9, 11 };
        fromLeftValues = new int[] { 1, 3, 5, 6, 8, 10, 11 };
        removed = 0;

        StartCoroutine(generateMaze());
        fillSecond();
        removeOuterslots();

        Debug.Log("remaning" + RemainingX.Count);
        parentScript = GetComponentInParent<Spawner>();
        try
        {
            checkRemains();
        }
        catch { }
        // Check if the parentScript is found

        try
        {
            parentScript.collate(pathX, pathZ, slotInMaze);
        }
        catch { }



    }

    void scatterObjects()
    {
        int randomIndex = Random.Range(0, pathX.Count);
        Vector3 randomPosition = new Vector3(pathX[randomIndex], 0, pathZ[randomIndex]);
        scatter1.transform.position = randomPosition;

    }

    void checkRemains()
    {
        Transform parentTransform = transform;
        int childCount = parentTransform.childCount;
        if (childCount < 49)
        {
            parentScript.delete();
        }


    }
    void removeDuplicates()
    {
        for (int i = pathX.Count - 1; i >= 0; i--)
        {
            clearToPass = true; // Reset clearToPass for each iteration

            for (int j = 0; j < toPassX.Count; j++)
            {
                if (pathZ[i] == toPassZ[j] && pathX[i] == toPassX[j])
                {
                    clearToPass = false;
                    break; // Exit the inner loop once a match is found
                }
            }

            if (clearToPass)
            {
                toPassX.Add(pathX[i]);
                toPassZ.Add(pathZ[i]);
                toPassSlot.Add(slotInMaze[i]);
            }
        }
    }
    void removeOuterslots()
    {
        for (int i = pathX.Count - 1; i >= 0; i--)
        {
            if (!notOutOfBounds(pathX[i], pathZ[i]))
            {
                destroySlot(pathX[i], pathZ[i]);
                pathX.RemoveAt(i);
                pathZ.RemoveAt(i);
                slotInMaze.RemoveAt(i);
            }
        }
    }


    void fillSecond()
    {
        for (int i = 0; i < RemainingX.Count; i++)
        {

            nextPositionX = RemainingX[i];
            nextPositionZ = RemainingZ[i];
            if (ifConnectToLeft(RemainingX[i], RemainingZ[i]))
            {
                swapSlot((RemainingX[i] - 5), RemainingZ[i], identifySlot((RemainingX[i] - 5), RemainingZ[i]), 4);
                generateSlot(fromLeft(), nextPositionX, nextPositionZ);
            }
            else if (ifConnectToRight(RemainingX[i], RemainingZ[i]))
            {
                swapSlot((RemainingX[i] + 5), RemainingZ[i], identifySlot((RemainingX[i] + 5), RemainingZ[i]), 2);
                generateSlot(fromRight(), nextPositionX, nextPositionZ);
            }
            else if (ifConnectToUp(RemainingX[i], RemainingZ[i]))
            {
                swapSlot(RemainingX[i], (RemainingZ[i] + 5), identifySlot(RemainingX[i], (RemainingZ[i] + 5)), 1);
                generateSlot(fromUp(), nextPositionX, nextPositionZ);
            }
            else if (ifConnectToDown(RemainingX[i], RemainingZ[i]))
            {
                swapSlot(RemainingX[i], (RemainingZ[i] - 5), identifySlot(RemainingX[i], (RemainingZ[i] - 5)), 3);
                generateSlot(fromDown(), nextPositionX, nextPositionZ);
            }

        }

    }


    IEnumerator generateMaze()
    {

        currentPositionZ = 0;
        currentPositionX = 0;
        mapSize = (xLength * zLength) - 1;
        emptySlot = mapSize;
        origMap = mapSize;
        remainingGridlocations();
        generateSlot(fromDown(), currentPositionX, currentPositionZ);
        currentPath(currentPositionX, currentPositionZ, currentSlot);
        Debug.Log("direction : " + pathDirection[0]);
        pathDirection.Add(getDirection(currentSlot));
        removeActualPath(GDirection, nextPositionX, nextPositionZ);
        for (int i = 0; i < mapSize; i++)
        {
            if (getPathDirection() == 1)
            {
                changePos();
                generateSlot(fromDown(), nextPositionX, nextPositionZ);
                Debug.Log(nextPositionX + "," + nextPositionZ);
                currentPath(nextPositionX, nextPositionZ, currentSlot);
                removeActualPath(GDirection, nextPositionX, nextPositionZ);
            }
            else if (getPathDirection() == 2)
            {
                changePos();
                generateSlot(fromLeft(), nextPositionX, nextPositionZ);
                Debug.Log(nextPositionX + "," + nextPositionZ);
                currentPath(nextPositionX, nextPositionZ, currentSlot);
                removeActualPath(GDirection, nextPositionX, nextPositionZ);
            }
            else if (getPathDirection() == 3)
            {
                changePos();
                generateSlot(fromUp(), nextPositionX, nextPositionZ);
                Debug.Log(nextPositionX + "," + nextPositionZ);
                currentPath(nextPositionX, nextPositionZ, currentSlot);
                removeActualPath(GDirection, nextPositionX, nextPositionZ);
            }
            else if (getPathDirection() == 4)
            {
                changePos();
                generateSlot(fromRight(), nextPositionX, nextPositionZ);
                Debug.Log(nextPositionX + "," + nextPositionZ);
                currentPath(nextPositionX, nextPositionZ, currentSlot);
                removeActualPath(GDirection, nextPositionX, nextPositionZ);
            }
            else
            {

                Debug.Log("Repositioning");
                loop = 0;
                while (repositioned == false && loop < 10)
                {
                    RepositionAt();
                    Debug.Log("delast" + currentSlot);
                    Debug.Log("repositioned path direction " + pathDirection[(pathDirection.Count - 1)]);
                    changePos();
                    loop++;
                }
                mapSize++;
                repositioned = false;

            }
            pathDirection.Add(getDirection(currentSlot));
            Debug.Log("x" + newPathX.Count);
            Debug.Log("z" + newPathZ.Count);
            Debug.Log("1:" + getPathDirection());
            Debug.Log("2:" + mapSize);
            Debug.Log(itlog);


            //yield return new WaitForSeconds(1.0f);

        }



        yield return null;


    }





    bool RepositionAt()
    {

        try
        {
            Debug.Log(nextDirection.Count);
            if (nextDirection[0] == 1)
            {
                if (checkIsNotAvailable(newPathX[0], (newPathZ[0] + 5)))
                {
                    nextDirection.RemoveAt(0);
                    newPathX.RemoveAt(0);
                    newPathZ.RemoveAt(0);
                }
                else
                {
                    nextPositionX = newPathX[0];
                    nextPositionZ = newPathZ[0];
                    GDirection = nextDirection[0];
                    nextDirection.RemoveAt(0);
                    newPathX.RemoveAt(0);
                    newPathZ.RemoveAt(0);
                    Debug.Log("repositioning direction up :" + nextPositionX + "and " + nextPositionZ);
                    generateSlot(fromDown(), nextPositionX, (nextPositionZ + 5));
                    Debug.Log("repositioned slot current from down " + currentSlot);
                    Debug.Log(nextPositionX + "," + nextPositionZ);
                    currentPath(nextPositionX, (nextPositionZ + 5), currentSlot);
                    repositioned = true;
                    return repositioned;


                }
            }

            else if (nextDirection[0] == 2)
            {
                if (checkIsNotAvailable((newPathX[0] + 5), newPathZ[0]))
                {
                    nextDirection.RemoveAt(0);
                    newPathX.RemoveAt(0);
                    newPathZ.RemoveAt(0);
                }
                else
                {
                    nextPositionX = newPathX[0];
                    nextPositionZ = newPathZ[0];
                    GDirection = nextDirection[0];
                    nextDirection.RemoveAt(0);
                    newPathX.RemoveAt(0);
                    newPathZ.RemoveAt(0);
                    Debug.Log("repositioning direction right :" + nextPositionX + "and " + nextPositionZ);
                    generateSlot(fromLeft(), (nextPositionX + 5), nextPositionZ);
                    Debug.Log("repositioned slot current from left " + currentSlot);
                    Debug.Log(nextPositionX + "," + nextPositionZ);
                    currentPath((nextPositionX + 5), nextPositionZ, currentSlot);
                    repositioned = true;
                    return repositioned;
                }


            }
            else if (nextDirection[0] == 3)
            {
                if (checkIsNotAvailable(newPathX[0], (newPathZ[0] - 5)))
                {
                    nextDirection.RemoveAt(0);
                    newPathX.RemoveAt(0);
                    newPathZ.RemoveAt(0);
                }
                else
                {
                    nextPositionX = newPathX[0];
                    nextPositionZ = newPathZ[0];
                    GDirection = nextDirection[0];
                    nextDirection.RemoveAt(0);
                    newPathX.RemoveAt(0);
                    newPathZ.RemoveAt(0);
                    Debug.Log("repositioning direction down :" + nextPositionX + "and " + nextPositionZ);
                    generateSlot(fromUp(), nextPositionX, (nextPositionZ - 5));
                    Debug.Log("repositioned slot current from up " + currentSlot);
                    Debug.Log(nextPositionX + "," + nextPositionZ);
                    currentPath(nextPositionX, (nextPositionZ - 5), currentSlot);
                    repositioned = true;
                    return repositioned;
                }


            }
            else if (nextDirection[0] == 4)
            {
                if (checkIsNotAvailable((newPathX[0] - 5), newPathZ[0]))
                {
                    nextDirection.RemoveAt(0);
                    newPathX.RemoveAt(0);
                    newPathZ.RemoveAt(0);
                }
                else
                {
                    nextPositionX = newPathX[0];
                    nextPositionZ = newPathZ[0];
                    GDirection = nextDirection[0];
                    nextDirection.RemoveAt(0);
                    newPathX.RemoveAt(0);
                    newPathZ.RemoveAt(0);
                    Debug.Log("repositioning direction left :" + nextPositionX + "and " + nextPositionZ);
                    generateSlot(fromRight(), (nextPositionX - 5), nextPositionZ);
                    Debug.Log("repositioned slot current from right " + currentSlot);
                    Debug.Log(nextPositionX + "," + nextPositionZ);
                    currentPath((nextPositionX - 5), nextPositionZ, currentSlot);
                    repositioned = true;
                    return repositioned;
                }


            }


            return false;

        }
        catch
        {

            fillGaps();
            return false;
            mapSize++;

        }

    }

    void checkEmpty()
    {
        int x = -1 * ((xLength * 5) / 2);
        int z = (zLength * 5);


        for (int i = 0; i < origMap; i++)
        {
            if (!checkIsNotAvailable(x, z))
            {
                RemainingX.Add(x);
                RemainingZ.Add(z);
            }
            if (x == (xLength * 5) / 2)
            {
                z = z - 5;
                x = -1 * ((xLength * 5) / 2);
            }
            else
            {
                x = x + 5;
            }

        }
    }



    void fillGaps()
    {
        try
        {

            for (int i = 0; i < RemainingX.Count; i++)
            {
                if (ifConnectToLeft(RemainingX[i], RemainingZ[i]))
                {
                    swapSlot((RemainingX[i] - 5), RemainingZ[i], identifySlot((RemainingX[i] - 5), RemainingZ[i]), 4);
                    nextPositionX = RemainingX[0];
                    nextPositionZ = RemainingZ[0];
                    break;
                }
                else if (ifConnectToRight(RemainingX[i], RemainingZ[i]))
                {
                    swapSlot((RemainingX[i] + 5), RemainingZ[i], identifySlot((RemainingX[i] + 5), RemainingZ[i]), 2);
                    nextPositionX = RemainingX[0];
                    nextPositionZ = RemainingZ[0];
                    break;
                }
                else if (ifConnectToUp(RemainingX[i], RemainingZ[i]))
                {
                    swapSlot(RemainingX[i], (RemainingZ[i] + 5), identifySlot(RemainingX[i], (RemainingZ[i] + 5)), 1);
                    nextPositionX = RemainingX[0];
                    nextPositionZ = RemainingZ[0];
                    break;
                }
                else if (ifConnectToDown(RemainingX[i], RemainingZ[i]))
                {
                    swapSlot(RemainingX[i], (RemainingZ[i] - 5), identifySlot(RemainingX[i], (RemainingZ[i] - 5)), 3);
                    nextPositionX = RemainingX[0];
                    nextPositionZ = RemainingZ[0];
                    break;
                }

            }
        }
        catch
        {
            checkEmpty();
        }
    }



    void sanitizePath()
    {
        for (int i = 0; i < newPathX.Count; i++)
        {
            if (notOutOfBounds(newPathX[i], newPathZ[i]))
            {
                Debug.Log("Sanitizing " + newPathX[i] + " :" + newPathZ[i]);
            }
            else
            {
                Debug.Log("Removing " + newPathX[i] + " :" + newPathZ[i]);
                newPathX.RemoveAt(i);
                newPathZ.RemoveAt(i);
                nextDirection.RemoveAt(i);
            }
        }
    }

    bool notOutOfBounds(int x, int z)
    {
        if (
            x <= (xLength / 2) * 5 &&
            x >= (xLength / 2) * -5 &&
            z >= 0 &&
            z <= zLength * 5
            ) { return true; }

        else
        {
            return false;
        }

    }





    void generateSlot(int slot, int xCoordinate, int zCoordinate)
    {
        if (checkIsNotAvailable(xCoordinate, zCoordinate))
        {
            Debug.Log("Not available to generate");
        }
        else
        {
            GameObject clone = Instantiate(slotReference(slot - 1), new Vector3(xCoordinate, 0, zCoordinate), Quaternion.identity, transform);
            instantiatedClones.Add(clone);
            Debug.Log(slotReference(slot - 1));
        }
    }

    GameObject slotReference(int slotPosition)
    {
        return (slotList[(slotPosition)]);
    }

    int fromDown()
    {
        pathDirection.Add(1);
        Dslot = fromDownValues[Random.Range(0, 6)];
        currentSlot = Dslot;
        Debug.Log("fromdown" + currentSlot);
        return (Dslot);


    }
    int fromRight()
    {

        pathDirection.Add(4);
        Rslot = fromRightValues[Random.Range(0, 6)];
        currentSlot = Rslot;
        Debug.Log("fromRight:" + currentSlot);
        return (Rslot);

    }
    int fromUp()
    {
        pathDirection.Add(3);
        Uslot = fromUpValues[Random.Range(0, 6)];
        currentSlot = Uslot;
        Debug.Log("fromup:" + currentSlot);
        return (Uslot);

    }
    int fromLeft()
    {
        pathDirection.Add(2);
        Lslot = fromLeftValues[Random.Range(0, 6)];
        currentSlot = Lslot;
        Debug.Log("fromLeft:" + currentSlot);
        return (Lslot);

    }


    void currentPath(int x, int z, int slotToAdd)
    {
        pathX.Add(x);
        pathZ.Add(z);
        slotInMaze.Add(slotToAdd);
        removeFromRemaining(x, z);
        removed++;
    }

    void remainingGridlocations()
    {
        int x = -1 * ((xLength / 2) * 5);
        int z = 0;
        for (int i = 0; i < mapSize; i++)
        {
            RemainingX.Add(x);
            RemainingZ.Add(z);
            if (x == ((xLength / 2) * 5))
            {
                x = -1 * ((xLength / 2) * 5);
                z += 5;
            }
            else
            {
                x += 5;
            }

        }
        Debug.Log("remaining count: " + RemainingX.Count);
    }

    void removeFromRemaining(int x, int z)
    {
        for (int i = 0; i < RemainingX.Count; i++)
        {
            if (RemainingX[i] == x && RemainingZ[i] == z)
            {
                RemainingX.RemoveAt(i);
                RemainingZ.RemoveAt(i);
                break;
            }
        }
    }



    int getDirection(int feedSlot)
    {
        maxPositionX = ((xLength / 2) * 5);
        maxPositionZ = (zLength * 5);
        possibleDirection = new List<int>();
        possibleDirection.Clear();
        possibleDirection.Add(1);
        possibleDirection.Add(2);
        possibleDirection.Add(3);
        possibleDirection.Add(4);
        Debug.Log("directions initial" + possibleDirection.Count);

        switch (feedSlot)
        {
            case 1:
                break;
            case 2:
                possibleDirection.Remove(1);
                possibleDirection.Remove(4);
                Debug.Log(feedSlot + "remove 1 and 4");
                break;
            case 3:
                possibleDirection.Remove(1);
                possibleDirection.Remove(2);
                Debug.Log(feedSlot + "remove 1 and 2");
                break;
            case 4:
                possibleDirection.Remove(2);
                possibleDirection.Remove(4);
                Debug.Log(feedSlot + "remove 2 and 4");
                break;
            case 5:
                possibleDirection.Remove(1);
                possibleDirection.Remove(3);
                Debug.Log(feedSlot + "remove 1 and 3");
                break;
            case 6:
                possibleDirection.Remove(2);
                possibleDirection.Remove(3);
                Debug.Log(feedSlot + "remove 2 and 3");
                break;
            case 7:
                possibleDirection.Remove(3);
                possibleDirection.Remove(4);
                Debug.Log(feedSlot + "remove 3 and 4");
                break;
            case 8:
                possibleDirection.Remove(3);
                Debug.Log(feedSlot + "remove 3");
                break;
            case 9:
                possibleDirection.Remove(4);
                Debug.Log(feedSlot + "remove 4");
                break;
            case 10:
                possibleDirection.Remove(1);
                Debug.Log(feedSlot + "remove 1");
                break;
            case 11:
                possibleDirection.Remove(2);
                Debug.Log(feedSlot + "remove 2");
                break;
        }
        Debug.Log("directions after switch" + possibleDirection.Count);


        if (nextPositionX == maxPositionX)
        {
            possibleDirection.Remove(2);
            Debug.Log("max X remove to right");
        }
        if (nextPositionX == (maxPositionX * -1))
        {
            possibleDirection.Remove(4);
            Debug.Log("max X remove left");
        }
        if (nextPositionZ == (0))
        {
            possibleDirection.Remove(3);
            Debug.Log("0 yz so remove down");
        }
        if (nextPositionZ == (maxPositionZ))
        {
            possibleDirection.Remove(1);
            Debug.Log("max z remove up");
        }

        Debug.Log("directions after edge constraints" + possibleDirection.Count);
        if (checkIsNotAvailable((nextPositionX - 5), nextPositionZ))
        {
            possibleDirection.Remove(4);
            Debug.Log("left is  occupied");

        }
        if (checkIsNotAvailable((nextPositionX + 5), nextPositionZ))
        {
            possibleDirection.Remove(2);
            Debug.Log("right is occupied");
        }
        if (checkIsNotAvailable(nextPositionX, (nextPositionZ + 5)))
        {
            possibleDirection.Remove(1);
            Debug.Log("up is  occupied");
        }
        if (checkIsNotAvailable(nextPositionX, (nextPositionZ - 5)))
        {
            possibleDirection.Remove(3);
            Debug.Log("down is  occupied");
        }
        Debug.Log("possibleDirection after checking" + possibleDirection.Count);
        addNextPossiblePath(); Debug.Log("trying to add possible path");
        if ((possibleDirection.Count) > 0)
        {
            GDirection = possibleDirection[Random.Range(0, possibleDirection.Count)];
            Debug.Log("direction" + GDirection);
            return (GDirection);
        }
        else
        {

            return (0);
        }



    }

    void addNextPossiblePath()
    {
        if (possibleDirection.Count > 1)
        {
            for (int i = 0; i < possibleDirection.Count; i++)
            {


                nextDirection.Add(possibleDirection[i]);
                Debug.Log("addded to next direction " + possibleDirection[i]);
                newPathX.Add(nextPositionX);
                Debug.Log("addded to next X " + nextPositionX);
                newPathZ.Add(nextPositionZ);
                Debug.Log("addded to next Z " + nextPositionZ);



            }
        }

    }



    void removeActualPath(int actualPath, int x, int z)
    {
        List<int> indicesToRemove = new List<int>();

        for (int i = 0; i < nextDirection.Count; i++)
        {
            if (nextDirection[i] == actualPath && newPathX[i] == x && newPathZ[i] == z)
            {
                Debug.Log("removing " + nextDirection[i] + " at X: " + newPathX[i] + " and Z: " + newPathZ[i]);
                indicesToRemove.Add(i);
            }
        }

        // Check if there are elements to remove
        if (indicesToRemove.Count > 0)
        {
            // Remove the elements outside the loop
            foreach (int index in indicesToRemove)
            {
                nextDirection.RemoveAt(index);
                newPathX.RemoveAt(index);
                newPathZ.RemoveAt(index);
            }
        }
    }
    void changePos()
    {
        switch (GDirection)
        {
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



    bool checkIsNotAvailable(int checkX, int checkZ)
    {
        for (int i = 0; i < pathX.Count; i++)
        {
            if (pathX[i] == checkX && pathZ[i] == checkZ)
            {
                return true;
            }
        }

        return false;
    }

    int getPathDirection()
    {
        if (pathDirection.Count > 0)
        {
            itlog = pathDirection[pathDirection.Count - 1];
            return itlog;
        }
        else
        {
            return 0;
        }
    }


    bool ifConnectToRight(int x, int z)
    {

        if (!checkIsNotAvailable(x, z) && checkIsNotAvailable((x + 5), z) && x >= (0 - gridX))
        {
            Debug.Log("connecting to Right");
            return true;
        }
        else
        {
            return false;
        }

    }
    bool ifConnectToUp(int x, int z)
    {

        if (!checkIsNotAvailable(x, z) && checkIsNotAvailable(x, (z + 5)) && z >= 0)
        {
            Debug.Log("connecting to Up");
            return true;
        }
        else
        {
            return false;
        }

    }
    bool ifConnectToLeft(int x, int z)
    {

        if (!checkIsNotAvailable(x, z) && checkIsNotAvailable((x - 5), z) && x <= (0 + gridX))
        {
            Debug.Log("connecting to left");
            return true;
        }
        else
        {
            return false;
        }

    }
    bool ifConnectToDown(int x, int z)
    {

        if (!checkIsNotAvailable(x, z) && checkIsNotAvailable((x), (z - 5)) && z <= gridZ)
        {
            Debug.Log("connecting to Down");
            return true;

        }
        else
        {
            return false;
        }

    }

    int identifySlot(int xGridSwap, int zGridSwap)
    {

        for (int i = 0; i < pathX.Count; i++)
        {
            if (pathX[i] == xGridSwap && pathZ[i] == zGridSwap)
            {
                slotToSwap = slotInMaze[i];
                pathX.RemoveAt(i);
                pathZ.RemoveAt(i);
                slotInMaze.RemoveAt(i);
            }
        }

        return slotToSwap;

    }


    void destroySlot(int destroyX, int destroyZ)
    {
        Vector3 positionToDestroy = new Vector3(destroyX, 0, destroyZ);

        // Loop through the list of instantiated clones
        for (int i = 0; i < instantiatedClones.Count; i++)
        {
            // Check if the position of the clone matches the specified position
            if (instantiatedClones[i].transform.position == positionToDestroy)
            {
                // Destroy the clone and remove it from the list
                Destroy(instantiatedClones[i]);
                instantiatedClones.RemoveAt(i);
                break; // Exit the loop since we found and destroyed the clone
            }
        }
    }


    void swapSlot(int swapX, int swapZ, int slotToReplace, int direction)
    {

        if (direction == 2)
        {
            nextDirection.Add(4);
            switch (slotToReplace)
            {

                case 2:
                    replacementSlot = 10;
                    break;
                case 4:
                    replacementSlot = 11;
                    break;
                case 7:
                    replacementSlot = 8;
                    break;
                case 9:
                    replacementSlot = 1;
                    break;

                default:

                    break;

            }
        }
        else if (direction == 1)
        {
            nextDirection.Add(3);
            switch (slotToReplace)
            {

                case 5:
                    replacementSlot = 10;
                    break;
                case 6:
                    replacementSlot = 11;
                    break;
                case 7:
                    replacementSlot = 9;
                    break;
                case 8:
                    replacementSlot = 1;
                    break;

                default:

                    break;

            }
        }

        else if (direction == 3)
        {
            nextDirection.Add(1);
            switch (slotToReplace)
            {

                case 2:
                    replacementSlot = 9;
                    break;
                case 3:
                    replacementSlot = 11;
                    break;
                case 5:
                    replacementSlot = 8;
                    break;
                case 10:
                    replacementSlot = 1;
                    break;

                default:

                    break;

            }
        }
        else if (direction == 4)
        {
            nextDirection.Add(2);

            switch (slotToReplace)
            {

                case 3:
                    replacementSlot = 10;
                    break;
                case 4:
                    replacementSlot = 9;
                    break;
                case 6:
                    replacementSlot = 8;
                    break;
                case 11:
                    replacementSlot = 1;
                    break;

                default:

                    break;

            }
        }

        destroySlot(swapX, swapZ);

        Instantiate(slotReference(replacementSlot - 1), new Vector3(swapX, 0, swapZ), Quaternion.identity, transform);

        swapPath(swapX, swapZ, replacementSlot);
        newPathX.Add(swapX);
        newPathZ.Add(swapZ);
    }

    void swapPath(int x, int z, int slot)
    {
        for (int i = 0; i < pathX.Count; i++)
        {
            if (x == pathX[i] && z == pathZ[i])
            {
                pathX.RemoveAt(i);
                pathZ.RemoveAt(i);
                slotInMaze.RemoveAt(i);
                break;
            }
        }

        pathX.Add(x);
        pathZ.Add(z);
        slotInMaze.Add(slot);


    }

    public Vector3 getVec()
    {
        int num = Random.Range(0, pathX.Count);
        Vector3 pos = new Vector3(pathX[num], 0, pathZ[num]);
        return pos;
    }




}








