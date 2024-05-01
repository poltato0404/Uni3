using UnityEngine;

public class chickinMaker : MonoBehaviour
{
    private GameObject chicken; // Store reference to instantiated chicken
    public float heightAbove = 1.0f; // Height above the script holder

    public void InstantiateChicken(GameObject chickPrefab)
    {
        if (chickPrefab == null)
        {
            Debug.LogError("Object to instantiate is not assigned.");
            return;
        }

        // Get the position of the script holder
        Vector3 scriptHolderPosition = transform.position;

        // Calculate the position to instantiate the object above the script holder
        Vector3 instantiatePosition = new Vector3(
            scriptHolderPosition.x,
            scriptHolderPosition.y + heightAbove,
            scriptHolderPosition.z
        );

        // Instantiate the GameObject at the new position
        chicken = Instantiate(chickPrefab, instantiatePosition, Quaternion.identity);
    }

    public void destroyChick()
    {
        if (chicken != null)
        {
            Destroy(chicken); // Destroy the instantiated chicken
            chicken = null; // Clear the reference
        }
        else
        {
            Debug.LogError("No chicken to destroy.");
        }
    }
}
