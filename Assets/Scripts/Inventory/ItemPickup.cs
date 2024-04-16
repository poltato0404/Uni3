using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public InventoryManager inventoryManager; // Reference to the InventoryManager
    
    void Pickup()
    {
        if (inventoryManager != null)
        {
            // Get the InventoryItem component from the clicked GameObject
            InventoryItem inventoryItem = GetComponent<InventoryItem>();
            if (inventoryItem != null)
            {
                // Add the item to the inventory
                inventoryManager.AddItemToInventory(inventoryItem);
                Debug.Log("Item picked up and added to inventory.");
            }
            else
            {
                Debug.LogWarning("No InventoryItem component found on the clicked GameObject.");
            }
        }
        else
        {
            Debug.LogWarning("InventoryManager reference is missing!");
        }

        Destroy(gameObject);
        Debug.Log("Item GameObject destroyed.");
    }

    private void OnMouseDown() // Corrected method name
    {
        Pickup();
        Debug.Log("Mouse down event detected.");
    }
}
