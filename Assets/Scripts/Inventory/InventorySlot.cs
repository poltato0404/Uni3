using UnityEngine;
using UnityEngine.UI;
using TMPro; // Add this line to import TextMeshPro namespace

public class InventorySlot : MonoBehaviour
{
    public Image imageIcon;
    public TextMeshProUGUI itemText; // Reference to TextMeshPro component

    public void AddItem(InventoryItem item)
    {
        imageIcon.sprite = item.itemIcon;
        imageIcon.enabled = true;

        // Update the text displayed by TextMeshPro
        itemText.text = item.itemName;

        Debug.Log("Item icon and text added to inventory slot.");
    }

    public void ClearSlot()
    {
        imageIcon.sprite = null;
        imageIcon.enabled = false;

        // Clear the text displayed by TextMeshPro
        itemText.text = "";

        Debug.Log("Inventory slot cleared.");
    }
}