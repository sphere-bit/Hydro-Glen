using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private UIInventory _inventoryUI;
    public int inventorySize = 10;

    // Start is called before the first frame update
    void Start()
    {
        _inventoryUI.initUIInventory(inventorySize);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (_inventoryUI.isActiveAndEnabled == false)
            {
                _inventoryUI.gameObject.SetActive(true);
            }
            else
            {
                _inventoryUI.gameObject.SetActive(false);
            }
        }
    }
}
