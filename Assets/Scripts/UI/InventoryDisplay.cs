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

    public void OnSlotClicked(SlotUI clickedSlot)
    {
        Debug.Log("OnSlotClicked(SlotUI clickedSlot)");
    }
}
