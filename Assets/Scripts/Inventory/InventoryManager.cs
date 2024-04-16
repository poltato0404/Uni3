using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] slots;

    // Add item to the first available slot
    public void AddItemToInventory(InventoryItem item)
    {
        foreach (InventorySlot slot in slots)
        {
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
}
