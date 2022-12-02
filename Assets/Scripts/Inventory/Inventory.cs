using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Inventory
{
    // System
    [SerializeField] private List<Slot> slots;
    public List<Slot> Slots => slots;
    public int Size => slots.Count;
    public UnityAction<Slot> OnInventorySlotChanged;

    public Inventory(int size)
    {
        slots = new List<Slot>(size);

        for (int i = 0; i < size; i++)
        {
            slots.Add(new Slot());
        }
    }

    public bool HasItemToAdd(ItemData itemData, int amount)
    {
        Debug.Log(itemData);
        // Check whether item exists in inventory
        if (ContainsItem(itemData, out List<Slot> slot))
        {
            foreach (var space in slot)
            {
                if (space.isAvailable(amount))
                {
                    Debug.Log("Space was available. Adding ...");
                    space.AddToSpace(amount);
                    OnInventorySlotChanged?.Invoke(space);
                    return true;
                }
            }
        }
        // Gets the first available slot
        if (HasFreeSlot(out Slot freeSlot))
        {
            Debug.Log("Inventory: HasFreeSlot(out Slot freeSlot)");
            freeSlot.Update(itemData, amount);
            OnInventorySlotChanged?.Invoke(freeSlot);
            return true;
        }
        // slots[0] = new Slot(itemData, amount);
        return false;
    }

    public bool ContainsItem(ItemData itemData, out List<Slot> slot)
    {
        slot = slots.Where(i => i.ItemData == itemData).ToList();
        return slot.Count >= 1 ? true : false;
    }

    public bool HasFreeSlot(out Slot freeSlot)
    {
        // find the first slot where the item data is null
        freeSlot = slots.FirstOrDefault(i => i.ItemData == null);
        // free slot is null when a free slot hasn't been found.
        // free slot is not null when a free slot has been found.
        return freeSlot == null ? false : true;
    }
}

// public class Inventory : MonoBehaviour
// {
//     [SerializeField] private Slot inventorySlot;
//     [SerializeField] private RectTransform contentPanel;
//     [SerializeField] private InventoryDetails inventoryDetails;
//     [SerializeField] private MouseFollower mouseFollower;
//     private List<Slot> slots = new List<Slot>();
//     // -1 = Outside of the bounds of the itemSlots list, meaning no item is being dragged.
//     private int nCurrDragItem = -1;
//     public Sprite sprite;
//     public int quantity;
//     public string title, description;
//     public Sprite sprite1;

//     public void Awake()
//     {
//         // Call when game menu is closed
//         inventoryDetails.ClearDescription();
//         ResetDragItem();
//     }

//     public void initInventory(int inventorySize)
//     {
//         for (int i = 0; i < inventorySize; i++)
//         {
//             Slot slot = new Slot();
//             slot.gameObject.SetActive(true);
//             slot.transform.SetParent(contentPanel);
//             slots.Add(slot);

//             // Assign UI events to the slot
//             slot.OnItemLeftPointerClick += HandleItemSelection;
//             slot.OnItemBeginDrag += HandleBeginDrag;
//             slot.OnItemEndDrag += HandleEndDrag;
//             slot.OnItemDrop += HandleSwap;
//             slot.OnItemRightPointerClick += HandleShowItemActions;
//         }
//     }

//     private void HandleShowItemActions(Slot itemSlot)
//     {
//         throw new NotImplementedException("HandleShowItemActions");
//     }
//     private void HandleEndDrag(Slot itemSlot)
//     {
//         ResetDragItem();
//         // mouseFollower.SetFollower(sprite, quantity);
//     }

//     private void ResetDragItem()
//     {
//         mouseFollower.Toggle(false);
//         nCurrDragItem = -1;
//     }
//     private void HandleBeginDrag(Slot itemSlot)
//     {
//         int nItem = slots.IndexOf(itemSlot);
//         // If found no reference of the item queried 
//         if (nItem == -1)
//         {
//             print("UIInventory HandleBeginDrag: Empty slot.");
//             return;
//         }
//         else
//         {
//             nCurrDragItem = nItem;
//         }
//         mouseFollower.Toggle(true);
//         mouseFollower.SetFollower(nItem == 0 ? sprite : sprite1, quantity);
//     }
//     private void HandleSwap(Slot itemSlot)
//     {
//         // nItemSlot is the slot index at which item will be placed.
//         // swap itemSlots of indices 'nItemSlot' and 'nCurrDragItem'
//         int nItemSlot = slots.IndexOf(itemSlot);
//         if (nItemSlot == -1)
//         {
//             // Swap an empty slot with the currently dragged item.

//             mouseFollower.Toggle(false);
//             nCurrDragItem = -1;
//             return;
//         }
//         // Use bubble swap
//         int nTmpSlot = nItemSlot;
//         // print("nItemSlot: " + nItemSlot + ", nCurrDragItem: " + nCurrDragItem);
//         // slots[nItemSlot] = slots[nCurrDragItem];
//         // slots[nCurrDragItem] = slots[nTmpSlot];
//         slots[nCurrDragItem].SetSlot(nItemSlot == 0 ? sprite : sprite1, quantity);
//         slots[nItemSlot].SetSlot(nCurrDragItem == 0 ? sprite : sprite1, quantity);
//         mouseFollower.Toggle(false);
//         nCurrDragItem = -1;
//     }

//     private void HandleItemSelection(Slot itemSlot)
//     {
//         inventoryDetails.SetDescription(sprite, title, description);
//         slots[0].SelectSlot();
//         Debug.Log(slots.IndexOf(itemSlot) + " clicked!");
//     }

//     private void ResetInventory()
//     {
//         // Call when menu is opened
//         inventoryDetails.ClearDescription();
//     }

//     // Start is called before the first frame update
//     void Start()
//     {
//         inventoryDetails.ClearDescription();
//         slots[0].SetSlot(sprite, quantity);
//         slots[1].SetSlot(sprite1, quantity);
//     }

//     // Update is called once per frame
//     void Update()
//     {
//     }
// }
