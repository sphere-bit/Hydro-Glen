using UnityEngine;
using UnityEngine.Events;

public class PlayerInventoryHolder : InventoryHolder
{
    // primary inventory is the player pocket.
    [SerializeField] protected int secondaryInventorySize; // backpack size
    [SerializeField] protected Inventory secondaryInventory; // backpack

    public Inventory SecondaryInventory => secondaryInventory;
    public static UnityAction<Inventory> OnPlayerBackpackDisplayRequested;
    protected override void Awake()
    {
        base.Awake();

        secondaryInventory = new Inventory(secondaryInventorySize);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            OnPlayerBackpackDisplayRequested?.Invoke(secondaryInventory);
        }
    }

    public bool HasItemToAdd(ItemData data, int amount)
    {
        if (primaryInventory.HasItemToAdd(data, amount))
        {
            return true;
        }
        else if (secondaryInventory.HasItemToAdd(data, amount))
        {
            return true;
        }

        return false;
    }
}
