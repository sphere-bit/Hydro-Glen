using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public abstract class InventoryHolder : MonoBehaviour
{
    [SerializeField] protected int offset = 9;
    [SerializeField] private int inventorySize;
    [SerializeField] protected Inventory primaryInventory;
    // int is the amount to offset the inventory display by
    public static UnityAction<Inventory, int> OnDynamicInventoryDisplayRequested;

    public int Offset => offset;
    public int InventorySize => inventorySize;
    public Inventory PrimaryInventory => primaryInventory;

    protected virtual void Awake()
    {
        primaryInventory = new Inventory(inventorySize);
        Persistence.OnLoadGame += LoadInventory;
    }

    protected abstract void LoadInventory(SaveData gameData);
}

[Serializable]
public struct InventorySaveData
{
    public Inventory inventory;
    public Vector3 position;
    public Quaternion rotation;
    public InventorySaveData(Inventory _inventory, Vector3 _position, Quaternion _rotation)
    {
        inventory = _inventory;
        position = _position;
        rotation = _rotation;
    }

    public InventorySaveData(Inventory _inventory)
    {
        inventory = _inventory;
        position = Vector2.zero;
        rotation = Quaternion.identity;
    }
}