using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour, IPointerClickHandler
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
        parentInventoryDisplay = transform.parent.GetComponent<InventoryDisplay>();
    }
    private void OnUISlotLeftClick()
    {
        // Access display class function
        parentInventoryDisplay?.OnSlotLeftClicked(this);
    }
    private void OnUISlotRightClick()
    {
        // Access display class function
        parentInventoryDisplay?.OnSlotRightClicked(this);
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
            ClearSlot();
        }

        if (slot.StackSize >= 1)
        {
            itemCount.text = slot.StackSize.ToString();
        }
        else
        {
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

    public void ClearSlot()
    {
        assignedSlot?.Clear();
        itemSprite.sprite = null;
        itemSprite.color = Color.clear;
        itemCount.text = "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnUISlotLeftClick();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnUISlotRightClick();
        }
    }
}
