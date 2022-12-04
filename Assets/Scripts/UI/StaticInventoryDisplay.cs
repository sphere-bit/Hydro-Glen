using System.Collections.Generic;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] private InventoryHolder inventoryHolder;
    [SerializeField] private SlotUI[] slots;
    protected override void Start()
    {
        base.Start();

        if (inventoryHolder != null)
        {
            inventory = inventoryHolder.PrimaryInventory;
            // Subscribe updateSlot function to the even 'OnInventorySlotChanged'
            inventory.OnInventorySlotChanged += UpdateSlot;
        }
        else
        {
            Debug.LogWarning($"No inventory assigned to {this.gameObject}");
        }

        AssignSlot(inventory);
    }
    public override void AssignSlot(Inventory inventoryToDisplay)
    {
        // throw new System.NotImplementedException();
        slotDict = new Dictionary<SlotUI, Slot>();

        if (slots.Length != inventory.Size)
        {
            Debug.Log($"Slots out of sync on {this.gameObject}");
        }

        for (int i = 0; i < inventory.Size; i++)
        {
            slotDict.Add(slots[i], inventory.Slots[i]);
            slots[i].Init(inventory.Slots[i]);
        }
    }
}
