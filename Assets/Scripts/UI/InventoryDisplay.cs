using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData mouseInventoryItem;
    protected Inventory inventory;
    public Dictionary<SlotUI, Slot> slotDict;

    public Inventory Inventory => inventory;
    public Dictionary<SlotUI, Slot> SlotDict => slotDict;

    public abstract void AssignSlot(Inventory inventoryToDisplay);
    protected virtual void UpdateSlot(Slot updatedSlot)
    {
        foreach (var slot in slotDict)
        {
            if (slot.Value == updatedSlot)
            {
                slot.Key.UpdateSlotUI(updatedSlot);
            }
        }
    }
    protected virtual void Start()
    {
    }

    public void OnSlotClicked(SlotUI clickedUISlot)
    {
        // Clicked slot (assigned slot) has an item; mouse doesn't have an item:
        if (clickedUISlot.AssignedSlot.ItemData != null && mouseInventoryItem.assignedSlot.ItemData == null)
        {
            // pick up the item of the slot.
            mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedSlot);
            clickedUISlot.ClearSlot();
            return;
        }

        // Clicked slot doesn't have an item; mouse has an item:
        if (clickedUISlot.AssignedSlot.ItemData == null && mouseInventoryItem.assignedSlot.ItemData != null)
        {
            Debug.Log("Clicked slot doesn't have an item; mouse has an item:");
            // place the clicked slot into the empty slot. Remove from mouse and fill slot.
            clickedUISlot.AssignedSlot.AssignItem(mouseInventoryItem.assignedSlot);
            clickedUISlot.UpdateSlotUI();

            mouseInventoryItem.ClearSlot();
        }
        // Debug.Log("OnSlotClicked(clickedUISlot clickedSlot)");
    }
}
