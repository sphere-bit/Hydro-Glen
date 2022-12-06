using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    public DynamicInventoryDisplay chestPanel;
    public DynamicInventoryDisplay playerBackpackPanel;

    public void Awake()
    {
        chestPanel.gameObject.SetActive(false);
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

    void DisplayInventory(Inventory inventory)
    {
        Debug.Log($"Interact key was pressed. Inventory shown. Panel: {chestPanel.name}");
        chestPanel.gameObject.SetActive(true);
        chestPanel.RefreshDynamicInventory(inventory);
    }
    void DisplayPlayerBackpack(Inventory inventory)
    {
        Debug.Log($"Interact was pressed. Backpack Inventory shown. Panel: {chestPanel.name}");
        playerBackpackPanel.gameObject.SetActive(true);
        playerBackpackPanel.RefreshDynamicInventory(inventory);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (chestPanel.gameObject.activeInHierarchy)
            {
                chestPanel.gameObject.SetActive(false);
            }

            if (playerBackpackPanel.gameObject.activeInHierarchy)
            {
                playerBackpackPanel.gameObject.SetActive(false);
            }
        }

    }
}
