using System;
using UnityEngine;

[Serializable]
public class Slot : ISerializationCallbackReceiver
{
    // A slot is list which has space.
    [NonSerialized] private ItemData itemData;
    [SerializeField] private int itemId = -1;
    [SerializeField] private int stackSize; // How many items we have currently.

    public ItemData ItemData => itemData;
    public int StackSize => stackSize;

    public Slot(ItemData itemData, int stackSize)
    {
        this.itemData = itemData;
        this.itemId = itemData.Id;
        this.stackSize = stackSize;
    }

    public string generateId()
    {
        return $"Slot-{Guid.NewGuid().ToString("N")}";
    }
    public void UpdateSlot(ItemData itemData, int stackSize)
    {
        this.itemData = itemData;
        this.itemId = itemData.Id;
        this.stackSize = stackSize;
    }
    public Slot()
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        itemData = null;
        itemId = -1;
        stackSize = -1;
    }

    public bool hasSpaceFor(int addAmount, out int amountRemaining)
    {
        amountRemaining = itemData.MaxStackSize - stackSize;
        return hasSpaceFor(addAmount);
    }

    public bool hasSpaceFor(int addAmount)
    {
        if (stackSize + addAmount <= itemData.MaxStackSize)
        {
            return true;
        }
        return false;
    }

    public void AddToSpace(int amount)
    {
        stackSize += amount;
    }

    public void RemoveFromSpace(int amount)
    {
        stackSize -= amount;
    }

    internal void AssignItem(Slot slot)
    {
        // Check whether the slot contains the same item.
        if (itemData == slot.ItemData)
        {
            // Combine
            AddToSpace(slot.stackSize);
        }
        else
        {
            // If items are different, swap with mouse's slot.
            itemData = slot.itemData;
            itemId = itemData.Id;
            stackSize = 0;
            AddToSpace(slot.stackSize);
        }
        // throw new NotImplementedException();
    }

    internal bool OnSplitStack(out Slot splitStack)
    {
        if (stackSize <= 1)
        {
            splitStack = null;
            return false;
        }

        int halfStack = Mathf.RoundToInt(stackSize / 2);
        Debug.Log($"prev {stackSize}");
        RemoveFromSpace(halfStack);
        Debug.Log($"curr {stackSize}");
        splitStack = new Slot(itemData, halfStack);

        return true;
    }

    internal bool OnSplitStack(bool isShiftKey, out Slot splitStack)
    {
        // Called by InventoryDisplay on slot clicked by right mouse button
        if (stackSize <= 1)
        {
            splitStack = null;
            return false;
        }

        // RMB + shift
        if (!isShiftKey)
        {
            Debug.Log($"one split");
            RemoveFromSpace(1);
            splitStack = new Slot(itemData, 1);
        }
        // RMB only
        else
        {
            Debug.Log($"half split");
            int halfStack = Mathf.RoundToInt(stackSize / 2);
            RemoveFromSpace(halfStack);
            splitStack = new Slot(itemData, halfStack);
        }

        return true;
    }

    public void OnBeforeSerialize()
    {

    }

    public void OnAfterDeserialize()
    {
        if (itemId == -1)
        {
            return;
        }

        // load resources from folder
        var db = Resources.Load<Database>("Database");
        itemData = db.GetItem(itemId);

    }
}
// public class Slot : IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
// {
//     [SerializeField] private Image image;
//     [SerializeField] private TMP_Text quantity;
//     [SerializeField] private Image selected;
//     public event Action<Slot> OnItemDrop, OnItemBeginDrag, OnItemEndDrag, OnItemRightPointerClick, OnItemLeftPointerClick;
//     private bool isEmpty = true;

//     public Slot(Image image, TMP_Text quantity, Image selected, bool isEmpty)
//     {
//         this.image = image;
//         this.quantity = quantity;
//         this.selected = selected;
//         this.isEmpty = isEmpty;
//     }

//     public void Awake()
//     {
//         ClearSlot();
//         DeselectSlot();
//     }
//     public void SelectSlot()
//     {
//         selected.enabled = true;
//     }
//     public void DeselectSlot()
//     {
//         selected.enabled = false;
//     }

//     public void SetSlot(Sprite itemSprite, int item_quantity)
//     {
//         image.gameObject.SetActive(true);
//         image.sprite = itemSprite;
//         quantity.text = item_quantity.ToString();
//         isEmpty = false;
//     }
//     private void ClearSlot()
//     {
//         image.gameObject.SetActive(false);
//         isEmpty = true;
//     }

//     public void OnPointerClick(PointerEventData pointerData)
//     {
//         // if (isEmpty)
//         // {
//         //     print("Slot OnPointerClick: Empty slot.");
//         //     return;
//         // }
// if (pointerData.button == PointerEventData.InputButton.Right)
// {
//     OnItemRightPointerClick?.Invoke(this);
// }
// else
// {
//     OnItemLeftPointerClick?.Invoke(this);
// }
//     }

//     public void OnBeginDrag(PointerEventData eventData)
//     {
//         // if (isEmpty)
//         // {
//         //     print("Slot OnBeginDrag: Empty slot.");
//         //     return;
//         // }
//         OnItemBeginDrag?.Invoke(this);
//     }

//     public void OnEndDrag(PointerEventData eventData)
//     {
//         OnItemEndDrag?.Invoke(this);
//     }

//     public void OnDrop(PointerEventData eventData)
//     {
//         OnItemDrop?.Invoke(this);
//     }

//     public void OnDrag(PointerEventData eventData)
//     {
//         // This is needed to handle dragging an item. Leave empty.
//     }
// }
