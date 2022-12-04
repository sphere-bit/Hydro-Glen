using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int size;
    [SerializeField] protected Inventory inventory;
    public static UnityAction<Inventory> OnDynamicInventoryDisplayRequested;

    public int Size => size;
    public Inventory Inventory => inventory;

    private void Awake()
    {
        inventory = new Inventory(size);
    }
}
