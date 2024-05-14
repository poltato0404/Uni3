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
    [SerializeField] GameObject docu;
    NavMeshSurface navMeshSurface;
    [SerializeField] GameObject guardAgent;
    public List<Vector3> coinList;
    [SerializeField] GameObject coin;
    [SerializeField] GameObject laptop;
    public List<Vector3> docuList;
    NavMeshAgent navMeshAgent;
    public bool gotLaptop;
    void Awake()
    {
        slotList = new List<GameObject> { slot1, slot2, slot3, slot4, slot5, slot6, slot7, slot8, slot9, slot10, slot11 };

    }
    public void SaveData(ref GameData data)
    {
        data.isLaptopRetrieved = gotLaptop;
        switch (data.currentLevel)
        {
            case 1: data.loadedLevel1 = true; break;
            case 2: data.loadedLevel2 = true; break;
            case 3: data.loadedLevel3 = true; break;
        }

        data.coins = coinList;
        data.docuList = docuList;


    }
    public void LoadData(GameData data)
    {
        gotLaptop = data.isLaptopRetrieved;

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
        switch (data.currentLevel)
        {
            case 1:
                break;
            case 2:
                instantiateGuards(data.guard1Pos, 1);
                break;
            case 3:
                instantiateGuards(data.guard1Pos, 1);
                instantiateGuards(data.guard2Pos, 2);
                break;
        }


        index = Random.Range(0, data.slotPosition.Count);
        pos = data.slotPosition[index];
        pos.y = 1;

        switch (data.currentLevel)
        {
            case 1:
                if (data.loadedLevel1)
                {
                    coinList = data.coins;
                    docuList = data.docuList;
                    if (!data.isLaptopRetrieved)
                    {
                        Instantiate(laptop, data.devicePos, Quaternion.identity);
                        Debug.Log("level1 loaded device : " + data.loadedLevel1);
                    }

                    for (int i = 0; i < data.coins.Count; i++)
                    {
                        Instantiate(coin, data.coins[i], Quaternion.identity);
                    }
                    for (int i = 0; i < data.docuList.Count; i++)
                    {
                        Instantiate(docu, data.docuList[i], Quaternion.identity);
                    }
                }
                else
                {
                    if (!data.isLaptopRetrieved)
                    {
                        Instantiate(laptop, pos, Quaternion.identity);
                        data.devicePos = pos;
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        index = Random.Range(1, data.slotPosition.Count);
                        pos = data.slotPosition[index];
                        pos.y = 1;
                        Instantiate(coin, pos, Quaternion.identity);
                        coinList.Add(pos);
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        index = Random.Range(1, data.slotPosition.Count);
                        pos = data.slotPosition[index];
                        pos.y = 1;
                        Instantiate(docu, pos, Quaternion.identity);
                        docuList.Add(pos);
                    }
                }
                break;
            case 2:

                if (data.loadedLevel2)
                {
                    coinList = data.coins;
                    docuList = data.docuList;


                    for (int i = 0; i < data.coins.Count; i++)
                    {
                        Instantiate(coin, data.coins[i], Quaternion.identity);
                    }
                    for (int i = 0; i < data.docuList.Count; i++)
                    {
                        Instantiate(docu, data.docuList[i], Quaternion.identity);
                    }
                }
                else
                {

                    for (int i = 0; i < 5; i++)
                    {
                        index = Random.Range(1, data.slotPosition.Count);
                        pos = data.slotPosition[index];
                        pos.y = 1;
                        Instantiate(coin, pos, Quaternion.identity);
                        coinList.Add(pos);
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        index = Random.Range(1, data.slotPosition.Count);
                        pos = data.slotPosition[index];
                        pos.y = 1;
                        Instantiate(docu, pos, Quaternion.identity);
                        docuList.Add(pos);
                    }
                }



                break;
            case 3:
                if (data.loadedLevel3)
                {
                    coinList = data.coins;
                    docuList = data.docuList;

                    for (int i = 0; i < data.coins.Count; i++)
                    {
                        Instantiate(coin, data.coins[i], Quaternion.identity);
                    }
                    for (int i = 0; i < data.docuList.Count; i++)
                    {
                        Instantiate(docu, data.docuList[i], Quaternion.identity);
                    }
                }
                else
                {

                    for (int i = 0; i < 7; i++)
                    {
                        index = Random.Range(1, data.slotPosition.Count);
                        pos = data.slotPosition[index];
                        pos.y = 1;
                        Instantiate(coin, pos, Quaternion.identity);
                        coinList.Add(pos);
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        index = Random.Range(1, data.slotPosition.Count);
                        pos = data.slotPosition[index];
                        pos.y = 1;
                        Instantiate(docu, pos, Quaternion.identity);
                        docuList.Add(pos);
                    }
                }

                break;
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
