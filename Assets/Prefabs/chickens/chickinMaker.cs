using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chickinMaker : MonoBehaviour
{
    GameObject chicken;
    public float heightAbove = 1.0f; // Height above the script holder

    public void InstantiateChicken(GameObject chick)
    {
        // Get the position of the script holder
        Vector3 scriptHolderPosition = transform.position;

        // Calculate the position to instantiate the object above the script holder
        Vector3 instantiatePosition = new Vector3(
            scriptHolderPosition.x,
            scriptHolderPosition.y + heightAbove,
            scriptHolderPosition.z
        );

        // Instantiate the GameObject at the new position
        if (chick != null)
        {
            Instantiate(chick, instantiatePosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Object to instantiate is not assigned.");
        }

        chicken = chick;
    }

    public void destroyChick()
    {
        Destroy(chicken);
    }
    
}
