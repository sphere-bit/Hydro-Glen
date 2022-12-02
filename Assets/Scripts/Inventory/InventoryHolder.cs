using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int size;
    public int Size => size;
    [SerializeField] protected Inventory inventory;
    public Inventory Inventory => inventory;
    public static UnityAction<Inventory> OnDynamicInventoryDisplayRequested;

    private void Awake()
    {
        inventory = new Inventory(size);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
