using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour, IDataPersistence
{
    public GameObject objectToInstantiate; // GameObject to instantiate
    private Vector3 pos = new Vector3(0f, 0f, 0f); // Position to instantiate objects
    [SerializeField] private List<Vector3> transformedVector;
    [SerializeField] private List<int> slots;
    private int currentLevel;




    void startManager()
    {
        Instantiate(objectToInstantiate, pos, Quaternion.identity, transform);
        StartCoroutine(WaitAndDoSomething());
    }
    IEnumerator WaitAndDoSomething()
    {
        mapGeneration Script = GetComponentInChildren<mapGeneration>();
        switch (currentLevel)
        {
            case 1: Script.xLength = 6; Script.zLength = 6; break;

            case 2: Script.xLength = 7; Script.zLength = 7; break;

            case 3: Script.xLength = 8; Script.zLength = 8; break;


        }
        Script.startGenerating();
        yield return new WaitForSeconds(3); // Wait for 5 seconds
        collate(Script.pathX, Script.pathZ, Script.slotInMaze);
        SceneManager.LoadScene("level1");
    }


    public void Check(int i)
    {
        if (i != 0)
        {
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("gen");
            foreach (GameObject obj in objectsWithTag)
            {
                Destroy(obj);
            }

            Instantiate(objectToInstantiate, pos, Quaternion.identity, transform);
        }
    }
    public void delete()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("gen");
        Instantiate(objectToInstantiate, pos, Quaternion.identity, transform);
    }

    public void SaveData(ref GameData data)
    {
        data.slotPosition = transformedVector;
        data.slotReference = slots;
        switch (data.currentLevel)
        {
            case 1: data.loadedLevel1 = false; break;
            case 2: data.loadedLevel2 = false; break;
            case 3: data.loadedLevel3 = false; break;

        }
    }
    public void LoadData(GameData data)
    {
        Debug.Log("loadedlevel" + data.loadedLevel1 + data.currentLevel);
        data.loadedLevel1 = false;
        currentLevel = data.currentLevel;
        startManager();

    }

    public void collate(List<int> xpos, List<int> zpos, List<int> slotToAdd)
    {
        transformedVector = new List<Vector3>();
        slots = new List<int>();
        Vector3 temp;
        for (int i = 0; i < xpos.Count; i++)
        {
            temp = new Vector3(xpos[i], 0, zpos[i]);
            transformedVector.Add(temp);
            slots.Add(slotToAdd[i]);
        }
    }



}
