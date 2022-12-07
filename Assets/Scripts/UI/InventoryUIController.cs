using UnityEngine;
using UnityEngine.Serialization;

public class InventoryUIController : MonoBehaviour
{
    [FormerlySerializedAs("chestPanel")] public DynamicInventoryDisplay inventoryPanel;
    // public DynamicInventoryDisplay inventoryPanel;
    public DynamicInventoryDisplay playerBackpackPanel;

    public void Awake()
    {
        inventoryPanel.gameObject.SetActive(false);
        playerBackpackPanel.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    private void OnEnable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
        PlayerInventoryHolder.OnPlayerBackpackDisplayRequested += DisplayPlayerBackpack;
    }
    private void OnDisable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;
        PlayerInventoryHolder.OnPlayerBackpackDisplayRequested -= DisplayPlayerBackpack;
    }

    void DisplayInventory(Inventory inventory, int offset)
    {
        Debug.Log($"Interact key was pressed. Inventory shown. Panel: {inventoryPanel.name}");
        inventoryPanel.gameObject.SetActive(true);
        inventoryPanel.RefreshDynamicInventory(inventory, offset);
    }
    void DisplayPlayerBackpack(Inventory inventory, int offset)
    {
        Debug.Log($"Interact was pressed. Backpack Inventory shown. Panel: {playerBackpackPanel.name}");
        playerBackpackPanel.gameObject.SetActive(true);
        playerBackpackPanel.RefreshDynamicInventory(inventory, offset);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inventoryPanel.gameObject.activeInHierarchy)
            {
                inventoryPanel.gameObject.SetActive(false);
            }

            if (playerBackpackPanel.gameObject.activeInHierarchy)
            {
                playerBackpackPanel.gameObject.SetActive(false);
            }
        }

    }
}
