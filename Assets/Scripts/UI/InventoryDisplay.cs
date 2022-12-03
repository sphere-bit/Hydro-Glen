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

        // Check whether clicked slot is the same as the assigned
        if (clickedUISlot.AssignedSlot.SlotId == mouseInventoryItem.assignedSlot.SlotId)
        {
            if (clickedUISlot.AssignedSlot.ItemData == null && mouseInventoryItem.assignedSlot.ItemData != null)
            {
                // Inventory cloneInventory = new Inventory(inventory.Slots, inventory.Size);
                // cloneInventory.Slots.Remove(clickedUISlot.AssignedSlot);

                // Split mouse slot
                // Don't stack on another item and not on the same clicked slot.
                if (inventory.HasFreeSlotExcept(clickedUISlot.AssignedSlot, out Slot freeSlot))
                {
                    // free slot should not be the same as clicked slot.
                    Debug.Log($"clickedUISlot.AssignedSlot.id:{clickedUISlot.AssignedSlot.SlotId}, freeSlot.id:{freeSlot.SlotId}");
                    if (mouseInventoryItem.assignedSlot.OnSplitStack(out Slot splitStack))
                    {
                        // mouseInventoryItem.UpdateMouseSlot(splitStack);

                        // Move the split stack to a free slot only 
                        if (splitStack != null)
                        {
                            freeSlot.Update(splitStack.ItemData, splitStack.StackSize);
                            inventory.OnInventorySlotChanged?.Invoke(freeSlot);
                        }
                    }
                }
            }
        }
        // Clicked slot doesn't have an item; mouse has an item:
        if (clickedUISlot.AssignedSlot.ItemData == null && mouseInventoryItem.assignedSlot.ItemData != null)
        {
            // place the clicked slot into the empty slot. Remove from mouse and fill slot.
            clickedUISlot.AssignedSlot.AssignItem(mouseInventoryItem.assignedSlot);
            clickedUISlot.UpdateSlotUI();
            mouseInventoryItem.ClearSlot();

            return;
        }
        // Debug.Log("OnSlotClicked(clickedUISlot clickedSlot)");

        // Both slots have an item
        if (clickedUISlot.AssignedSlot.ItemData != null && mouseInventoryItem.assignedSlot.ItemData != null)
        {
            bool isSameItem = clickedUISlot.AssignedSlot.ItemData == mouseInventoryItem.assignedSlot.ItemData;

            // Items in both slots are the same then combine items to assigned slot
            // Check whether slot stack size + mouse stack size <= max stack size:
            if (isSameItem && clickedUISlot.AssignedSlot.isAvailable(mouseInventoryItem.assignedSlot.StackSize))
            {
                // True: Let assigned slot take count from mouse slot. Add quantity to space.
                clickedUISlot.AssignedSlot.AssignItem(mouseInventoryItem.assignedSlot);
                clickedUISlot.UpdateSlotUI();

                // After combining, clear
                mouseInventoryItem.ClearSlot();
                return;
            }
            else if (isSameItem && !clickedUISlot.AssignedSlot.isAvailable(mouseInventoryItem.assignedSlot.StackSize, out int amountRemaining))
            {
                // When slot is full
                if (amountRemaining < 1)
                {
                    // Swap full stack with unfilled stack
                    SwapSlot(clickedUISlot);
                    return;
                }
                // When slot has some space left
                else
                {
                    int mouseAmountRemaining = mouseInventoryItem.assignedSlot.StackSize - amountRemaining;
                    clickedUISlot.AssignedSlot.AddToSpace(amountRemaining);
                    clickedUISlot.UpdateSlotUI();

                    // Update mouse with amount remaining after giving to the clicked slot.
                    var newSlot = new Slot(mouseInventoryItem.SlotId, mouseInventoryItem.assignedSlot.ItemData, mouseAmountRemaining);
                    mouseInventoryItem.ClearSlot();
                    mouseInventoryItem.UpdateMouseSlot(newSlot);
                    return;
                }
            }
            // If items in both slots are different then swap.
            else if (!isSameItem)
            {
                SwapSlot(clickedUISlot);
                return;
            }
        }
    }

    private void SwapSlot(SlotUI clickedUISlot)
    {
        // Use bubble swap to swap clicked and assigned slots.
        var cloneSlot = new Slot(mouseInventoryItem.SlotId, mouseInventoryItem.assignedSlot.ItemData, mouseInventoryItem.assignedSlot.StackSize);
        mouseInventoryItem.ClearSlot();

        // Pass in the clicked slot's data
        mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedSlot);
        clickedUISlot.ClearSlot();

        clickedUISlot.AssignedSlot.AssignItem(cloneSlot);
        clickedUISlot.UpdateSlotUI();
    }
}
