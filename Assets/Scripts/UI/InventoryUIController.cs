using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    public DynamicInventoryDisplay inventoryPanel;

    public void Awake()
    {
        inventoryPanel.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    private void OnEnable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
    }
    private void OnDisable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;
    }

    void DisplayInventory(Inventory inventory)
    {
        Debug.Log($"B was pressed. Inventory shown. Panel: {inventoryPanel.name}");
        inventoryPanel.gameObject.SetActive(true);
        inventoryPanel.RefreshDynamicInventory(inventory);
    }

    // Update is called once per frame
    void Update()
    {
        if (!inventoryPanel.gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.B))
        {
            // Open bag
            DisplayInventory(new Inventory(30));
        }
        else if (inventoryPanel.gameObject.activeInHierarchy && (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.Escape)))
        {
            Debug.Log("Closing bag...");
            // Close bag
            inventoryPanel.gameObject.SetActive(false);
        }
    }
}
