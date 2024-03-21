using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level1gen : MonoBehaviour
{
    public SceneData sceneData; // Reference to the ScriptableObject containing preset values
    public List<GameObject> prefabToInstantiate;
    [SerializeField] private GameObject slot1, slot2, slot3, slot4, slot5, slot6, slot7,slot8, slot9, slot10, slot11;
    // Start is called before the first frame update
    void Start()
    {
        prefabToInstantiate = new List<GameObject>{slot1, slot2, slot3, slot4, slot5, slot6, slot7, slot8, slot9, slot10, slot11};
        for (int i = 0; i < sceneData.list1.Count; i++)
            {
                // Get values from the ScriptableObject
                int value1 = sceneData.list1[i];
                int value2 = sceneData.list2[i];
                int value3 = sceneData.list3[i];

                // Create a position based on the retrieved values
                Vector3 position = new Vector3(value1, 0, value2);

                // Instantiate the prefab GameObject at the calculated position
                Instantiate(prefabToInstantiate[value3-1], position, Quaternion.identity);
            }
    }

   
}
