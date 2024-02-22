using UnityEngine;

public class PlantInteraction : MonoBehaviour
{
    void Update()
    {
        // Check for interaction input (adjust the key as needed)
        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractWithPlant();
        }
    }

    void InteractWithPlant()
    {
        // Raycast from the camera to detect the plant
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject plant = hit.collider.gameObject;
            // Add your interaction logic here
            Debug.Log("Interacting with plant: " + plant.name);
        }
    }
}
