using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] slots;
    public List<InventoryItem> itemsInInventory;
    void Start() { itemsInInventory = new List<InventoryItem>(); }



    // Add item to the first available slot
    public void AddItemToInventory(InventoryItem item)
    {
        foreach (InventorySlot slot in slots)
        {
            if (IsItemInInventory(item))
            {
                Debug.Log("Item is already in the inventory.");
                return;
            }
            if (!slot.imageIcon.enabled)
            {

                slot.AddItem(item);

                Debug.Log("Item added to inventory slot.");
                return;
            }
        }
        Debug.Log("Inventory full, cannot add item.");

    }

    // Clear all slots
    public void ClearInventory()
    {
        foreach (InventorySlot slot in slots)
        {
            slot.ClearSlot();
            Debug.Log("Inventory slot cleared.");
        }

    }
    private bool IsItemInInventory(InventoryItem item)
    {
        foreach (InventoryItem inventoryItem in itemsInInventory)
        {
            if (inventoryItem == item)
            {
                return true;
            }
        }
        return false;
    }
}
