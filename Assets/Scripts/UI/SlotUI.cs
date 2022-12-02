using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemCount;
    [SerializeField] private Slot assignedSlot;
    private Button button;
    public InventoryDisplay parentInventoryDisplay { get; private set; }
    public Slot AssignedSlot => assignedSlot;

    private void Awake()
    {
        ClearSlot();

        button = GetComponent<Button>();
        button?.onClick.AddListener(OnUISlotClick);
        parentInventoryDisplay = transform.parent.GetComponent<InventoryDisplay>();
    }

    private void OnUISlotClick()
    {
        // Access display class function
        // throw new NotImplementedException("OnUISlotClick()");
        parentInventoryDisplay?.OnSlotClicked(this);
    }

    public void Init(Slot slot)
    {
        assignedSlot = slot;
        UpdateSlotUI(slot);
    }

    public void UpdateSlotUI(Slot slot)
    {
        if (slot.ItemData != null)
        {
            itemSprite.sprite = slot.ItemData.icon;
            itemSprite.color = Color.white;
        }
        else
        {
            Debug.Log("Cleared!");
            ClearSlot();
        }

        if (slot.StackSize >= 1)
        {
            itemCount.text = slot.StackSize.ToString();
        }
        else
        {
            Debug.Log("Cleared! slot.StackSize <= 1");
            ClearSlot();
        }
    }
    public void UpdateSlotUI()
    {
        if (assignedSlot != null)
        {
            UpdateSlotUI(assignedSlot);
        }
    }

    private void ClearSlot()
    {
        assignedSlot?.Clear();
        itemSprite.sprite = null;
        itemSprite.color = Color.clear;
        itemCount.text = "";
    }
}
