using System.Collections.Generic;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] private InventoryHolder inventoryHolder;
    [SerializeField] private SlotUI[] slots;

    private void OnEnable()
    {
        PlayerInventoryHolder.OnPlayerInventoryChanged += RefreshStaticDisplay;
    }

    private void OnDisable()
    {
        PlayerInventoryHolder.OnPlayerInventoryChanged -= RefreshStaticDisplay;
    }

    private void RefreshStaticDisplay()
    {
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

        AssignSlot(inventory, 0);
    }

    protected override void Start()
    {
        RefreshStaticDisplay();
    }
    public override void AssignSlot(Inventory inventoryToDisplay, int offset)
    {
        // throw new System.NotImplementedException();
        slotDict = new Dictionary<SlotUI, Slot>();

        // if (slots.Length != inventory.Size)
        // {
        //     Debug.Log($"Slots out of sync on {this.gameObject}");
        // }

        for (int i = 0; i < inventoryHolder.Offset; i++)
        {
            slotDict.Add(slots[i], inventory.Slots[i]);
            slots[i].Init(inventory.Slots[i]);
        }
    }
}
