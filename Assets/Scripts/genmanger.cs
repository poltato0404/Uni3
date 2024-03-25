using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public GameObject objectToInstantiate; // GameObject to instantiate
    private Vector3 pos = new Vector3(0f, 0f, 0f); // Position to instantiate objects
    public SceneData sceneData;
    public List<int> toPassX; 
    public List<int> toPassZ;
    public List<int> toPassSlot; 


    void Start()
    {
        toPassX= new List<int>();
        toPassZ = new List<int>();
        toPassSlot = new List<int>();
        Instantiate(objectToInstantiate, pos, Quaternion.identity, transform);
        StartCoroutine(WaitAndDoSomething());
    }
    IEnumerator WaitAndDoSomething()
    {
        yield return new WaitForSeconds(5); // Wait for 5 seconds
        passTolevel1();
        SceneManager.LoadScene("level1");
        Debug.Log("Waited for 5 seconds!");
    }
    void passTolevel1()
{
    if (sceneData.list1.Count < 49)
    {
        // Find all GameObjects with the tag "gen"
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("gen");

        // Loop through each found GameObject
        foreach (GameObject obj in objectsWithTag)
        {
            // Access the script on the child GameObject
            mapGeneration childScript = obj.GetComponent<mapGeneration>();

            // Check if the script exists
            if (childScript != null)
            {
                // Call the method on the child script and pass the list
                childScript.passToParent();
            }
            else
            {
                Debug.LogError("Child script not found on child GameObject.");
            }
        }
    }
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
    public void delete(){
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("gen");
        Instantiate(objectToInstantiate, pos, Quaternion.identity, transform);
    }

public void ReceiveListFromChild(List<int> xList,List<int> zList,List<int> slotList )
    {
    sceneData.list1.Clear();
    sceneData.list2.Clear();
    sceneData.list3.Clear();
    for (int i = 0; i < xList.Count; i++)
    {

        sceneData.list1.Add(xList[i]);      // Add an integer to list1
        sceneData.list2.Add(zList[i]);      // Add an integer to list2
        sceneData.list3.Add(slotList[i]); // Add an integer to list3
    }
    }


}
