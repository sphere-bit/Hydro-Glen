using UnityEngine;

[System.Serializable]
public class Slot
{
    // A slot is list which has space.
    [SerializeField] private ItemData itemData;
    public ItemData ItemData => itemData;
    [SerializeField] private int stackSize; // How many items we have currently.
    public int StackSize => stackSize;
    public Slot(ItemData itemData, int stackSize)
    {
        this.itemData = itemData;
        this.stackSize = stackSize;
    }

    public void Update(ItemData itemData, int stackSize)
    {
        this.itemData = itemData;
        this.stackSize = stackSize;
    }
    public Slot()
    {
        Clear();
    }

    public void Clear()
    {
        itemData = null;
        stackSize = -1;
    }

    public bool isAvailable(int addAmount, out int amountRemaining)
    {
        amountRemaining = itemData.maxStackSize - stackSize;
        return isAvailable(addAmount);
    }

    public bool isAvailable(int addAmount)
    {
        if (stackSize + addAmount <= itemData.maxStackSize)
        {
            Debug.Log(addAmount + " added");
            Debug.Log(stackSize + " stackSize");
            Debug.Log(addAmount + " addAmount");
            Debug.Log(itemData.maxStackSize + " itemData.maxStackSize");
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
        if (itemData == slot.ItemData)
        {
            // Combine
            AddToSpace(slot.stackSize);
        }
        else
        {
            // If items are different, swap with mouse's slot.
            itemData = slot.itemData;
            stackSize = 0;
            AddToSpace(slot.stackSize);
        }
        // throw new NotImplementedException();
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
//         if (pointerData.button == PointerEventData.InputButton.Right)
//         {
//             OnItemRightPointerClick?.Invoke(this);
//         }
//         else
//         {
//             OnItemLeftPointerClick?.Invoke(this);
//         }
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
