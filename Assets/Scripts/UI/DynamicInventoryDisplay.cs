using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DynamicInventoryDisplay : InventoryDisplay
{
    [SerializeField] protected SlotUI slotPrefab;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    public void RefreshDynamicInventory(Inventory inventoryToDisplay)
    {
        ClearSlots();
        inventory = inventoryToDisplay;
        AssignSlot(inventoryToDisplay);
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
