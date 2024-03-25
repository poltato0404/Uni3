using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scatter : MonoBehaviour
{
    // Start is called before the first frame update
    public mapGeneration list;
    private int num;
    public void startrelocation()
    {
        
        Vector3 randomPosition = list.getVec();

        // Apply the random position to the GameObject
        transform.position = randomPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
