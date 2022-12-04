using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int inventorySize;
    [SerializeField] protected Inventory primaryInventory;
    public static UnityAction<Inventory> OnDynamicInventoryDisplayRequested;

    public int InventorySize => inventorySize;
    public Inventory PrimaryInventory => primaryInventory;

    protected virtual void Awake()
    {
        primaryInventory = new Inventory(inventorySize);
    }
}
