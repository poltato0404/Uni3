using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class mapLoader : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject slot1, slot2, slot3, slot4, slot5, slot6, slot7, slot8, slot9, slot10, slot11;
    List<GameObject> slotList;
    [SerializeField]GameObject instantiatedPrefab;
    NavMeshSurface navMeshSurface;
    [SerializeField]GameObject guardAgent;
    NavMeshAgent navMeshAgent;
    void Awake()
    {
        slotList = new List<GameObject> { slot1, slot2, slot3, slot4, slot5, slot6, slot7, slot8, slot9, slot10, slot11 };
    }
    public void SaveData(ref GameData data)
    {
        
    }   
    public void LoadData(GameData data)
    {
        for(int i = 0; i < data.slotPosition.Count; i++)
        {
          Instantiate(slotReference(data.slotReference[i]), data.slotPosition[i], Quaternion.identity);
          
        }
        navMeshSurface = instantiatedPrefab.GetComponent<NavMeshSurface>();
        navMeshSurface.BuildNavMesh();
        navMeshAgent= guardAgent.GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = true;
    }
    GameObject slotReference(int slotPosition)
    {
        return (slotList[(slotPosition-1)]);
    }
}
