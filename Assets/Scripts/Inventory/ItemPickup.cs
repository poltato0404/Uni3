using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public InventoryItem inventoryItem;
    public InventoryManager inventoryManager; // Reference to the InventoryManager

    void Pickup()
    {
        if (inventoryManager != null)
        {
            inventoryManager.AddItemToInventory(inventoryItem);
        }
        else
        {
            Debug.LogWarning("InventoryManager reference is missing!");
        }

        Destroy(gameObject);
    }

    private void OnMouseDown() // Corrected method name
    {
        Pickup();
    }
}
