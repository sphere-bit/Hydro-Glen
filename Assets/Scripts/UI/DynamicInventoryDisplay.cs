using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DynamicInventoryDisplay : InventoryDisplay
{
    [SerializeField] protected SlotUI slotPrefab;


    // Start is called before the first frame update
    protected override void Start()
    {
        // InventoryHolder.OnDynamicInventoryDisplayRequested += RefreshDynamicInventory;
        base.Start();

        // Clear out inventory and setup. When accessing, show what is in inventory when game sets up
        // AssignSlot(inventory);
    }
    public override void AssignSlot(Inventory inventoryToDisplay)
    {
        slotDict = new Dictionary<SlotUI, Slot>();
        if (inventoryToDisplay == null)
        {
            return;
        }

        for (int i = 0; i < inventoryToDisplay.Size; i++)
        {
            // parent as transform
            var uiSlot = Instantiate(slotPrefab, transform);
            // pair ui slot and backend slot
            slotDict.Add(uiSlot, inventoryToDisplay.Slots[i]);
            uiSlot.Init(inventoryToDisplay.Slots[i]);
            uiSlot.UpdateSlotUI();
        }
    }

    private void OnDestroy()
    {
        // InventoryHolder.OnDynamicInventoryDisplayRequested += RefreshDynamicInventory;
    }

    public void RefreshDynamicInventory(Inventory inventoryToDisplay)
    {
        ClearSlots();
        inventory = inventoryToDisplay;
        AssignSlot(inventoryToDisplay);
    }

    private void ClearSlots()
    {
        // gets all children UI slots on transform
        foreach (var item in transform.Cast<Transform>())
        {
            Destroy(item.gameObject);
        }

        if (slotDict != null)
        {
            slotDict.Clear();
        }
    }
}
