using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image imageIcon;

    public void AddItem(InventoryItem item)
    {
        imageIcon.sprite = item.itemIcon;
        imageIcon.enabled = true;
    }

    public void ClearSlot()
    {
        imageIcon.sprite = null;
        imageIcon.enabled = false;
    }
}
