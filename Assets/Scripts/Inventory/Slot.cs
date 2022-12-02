// using System;
// using TMPro;
// using UnityEngine;
// using UnityEngine.EventSystems;
// using UnityEngine.UI;

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
