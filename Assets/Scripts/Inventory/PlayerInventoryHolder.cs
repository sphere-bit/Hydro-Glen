using UnityEngine;
using UnityEngine.Events;

public class PlayerInventoryHolder : InventoryHolder
{
    // primary inventory is the player pocket.
    public static UnityAction<Inventory, int> OnPlayerBackpackDisplayRequested;
    public static UnityAction OnPlayerInventoryChanged;

    // [SerializeField] protected int secondaryInventorySize; // backpack size
    // [SerializeField] protected Inventory secondaryInventory; // backpack

    // public Inventory SecondaryInventory => secondaryInventory;
    // protected override void Awake()
    // {
    //     base.Awake();

    //     secondaryInventory = new Inventory(secondaryInventorySize);
    // }
    private void Start()
    {
        SaveGameManager.gameData.playerInventory = new InventorySaveData(primaryInventory);
    }

    protected override void LoadInventory(SaveData gameData)
    {
        if (gameData.playerInventory.inventory != null)
        {
            this.primaryInventory = gameData.playerInventory.inventory;
            OnPlayerInventoryChanged?.Invoke();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            OnPlayerBackpackDisplayRequested?.Invoke(primaryInventory, offset);
        }
    }

    public bool HasItemToAdd(ItemData data, int amount)
    {
        if (primaryInventory.HasItemToAdd(data, amount))
        {
            return true;
        }
        // else if (secondaryInventory.HasItemToAdd(data, amount))
        // {
        //     return true;
        // }

        return false;
    }
}