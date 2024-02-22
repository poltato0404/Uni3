using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    void Update()
    {
        // Check for interaction input (adjust the key as needed)
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        int layerMask = LayerMask.GetMask("CollectibleOrgan");

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            Debug.Log("Hit: " + hit.collider.gameObject.name);

            if (hit.collider.CompareTag("CollectibleOrgan"))
            {
                Debug.Log("Collecting organ!");
                CollectOrgan(hit.collider.gameObject);
            }
            else
            {
                Debug.Log("Not a collectible organ!");
            }
        }
        else
        {
            Debug.Log("Raycast did not hit anything!");
        }
    }

    void CollectOrgan(GameObject organ)
    {
        Debug.Log("Collecting organ: " + organ.name);

        // Implement the logic to handle the collection of the organ
        organ.SetActive(false);

        Debug.Log("Organ collected!");
    }

}
