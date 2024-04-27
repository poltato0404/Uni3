using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class mapLoader : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject slot1, slot2, slot3, slot4, slot5, slot6, slot7, slot8, slot9, slot10, slot11;
    List<GameObject> slotList;
    [SerializeField] GameObject instantiatedPrefab;
    NavMeshSurface navMeshSurface;
    [SerializeField] GameObject guardAgent;
    [SerializeField] GameObject coin;
    [SerializeField] GameObject laptop;
    NavMeshAgent navMeshAgent;
    void Awake()
    {
        slotList = new List<GameObject> { slot1, slot2, slot3, slot4, slot5, slot6, slot7, slot8, slot9, slot10, slot11 };
    }
    public void SaveData(ref GameData data)
    {
        
        switch(data.currentLevel)
        {
            case 1: data.loadedLevel1 = true; break;
            case 2: data.loadedLevel2 = true; break;
            case 3: data.loadedLevel3 = true; break;
        }
        

    }
    public void LoadData(GameData data)
    {
        for (int i = 0; i < data.slotPosition.Count; i++)
        {
            Instantiate(slotReference(data.slotReference[i]), data.slotPosition[i], Quaternion.identity);

        }
        navMeshSurface = instantiatedPrefab.GetComponent<NavMeshSurface>();
        navMeshSurface.BuildNavMesh();
        navMeshAgent = guardAgent.GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = true;
        int index;
        Vector3 pos;
        int guards = data.currentLevel;
        
        for (int i = 1; i < guards+1; i++)
        {
            if (1 == i) { instantiateGuards(data.guard1Pos, i); }
            if (2 == i) { instantiateGuards(data.guard2Pos, i); }
            if (3 == i) { instantiateGuards(data.guard3Pos, i); }
        }
        index = Random.Range(0, data.slotPosition.Count);
        pos = data.slotPosition[index];
        pos.y = 1;
        switch(data.currentLevel)
        {
            case 1: 
                if (data.loadedLevel1)
                { 
                    Instantiate(laptop, data.devicePos, Quaternion.identity);  
                    Debug.Log("level1 loaded device : "+ data.loadedLevel1);
                } 
                else
                {
                    Instantiate(laptop, pos, Quaternion.identity);
                    data.devicePos = pos;
                }
                break;
            case 2: 
                if(data.loadedLevel2) 
                { 
                    Instantiate(laptop, data.devicePos, Quaternion.identity);  
                    Debug.Log("level1 loaded device : "+ data.loadedLevel2);
                } 
                else
                {
                    Instantiate(laptop, pos, Quaternion.identity);
                    data.devicePos = pos;
                }
                break;
            case 3: 
                if (data.loadedLevel3)
                 { 
                    Instantiate(laptop, data.devicePos, Quaternion.identity);  
                    Debug.Log("level1 loaded device : "+ data.loadedLevel3);
                } 
                 else
                {
                    Instantiate(laptop, pos, Quaternion.identity);
                    data.devicePos = pos;
                }
                break;
        }
        
       
        for (int i = 0; i < 5; i++)
        {
            index = Random.Range(0, data.slotPosition.Count);
            pos = data.slotPosition[index];
            pos.y = 1;

            Instantiate(coin, pos, Quaternion.identity);

        }

    }
    GameObject slotReference(int slotPosition)
    {
        return (slotList[(slotPosition - 1)]);
    }

    void instantiateGuards(Vector3 pos, int assign)
    {
        GameObject guard = Instantiate(guardAgent, pos, Quaternion.identity);
        ai script = guard.GetComponent<ai>();
        script.guardNumber = assign;
    }
}
