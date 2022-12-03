using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseItemData : MonoBehaviour
{
    public Image itemSprite;
    public TextMeshProUGUI itemCount;
    public Slot assignedSlot;

    public void UpdateMouseSlot(Slot slot)
    {
        assignedSlot.AssignItem(slot);
        itemSprite.sprite = slot.ItemData.Icon;
        itemCount.text = slot.StackSize.ToString();
        itemSprite.color = Color.white;
    }

    private void Awake()
    {
        itemSprite.color = Color.clear;
        itemCount.text = "";
    }

    private void Update()
    {
        // Has an item. 
        if (assignedSlot.ItemData != null)
        {
            // Make object follow mouse.
            transform.position = Input.mousePosition;

            // Check where primary (left) mouse button is clicked
            if (Input.GetMouseButton(0) && !IsPointerOverlapUIObject())
            {
                Debug.Log("Primary left click was pressed and no overlapping UI object.");
                ClearSlot();
            }
        }
    }

    public void ClearSlot()
    {
        assignedSlot.Clear();
        itemCount.text = "";
        itemSprite.sprite = null;
        itemSprite.color = Color.clear;
    }

    public static bool IsPointerOverlapUIObject()
    {
        // False: no UI hit by pointer raycast.
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Input.mousePosition;
        // Raycast grabs all items that are clickable by the mouse pointer.
        // Without raycast, clicks wouldn't be registered.
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }
}
//     [SerializeField]
//     private Canvas _canvas;

//     [SerializeField]
//     private Slot inventorySlot;
//     public void Awake()
//     {
//         _canvas = transform.root.GetComponent<Canvas>();
//         inventorySlot = GetComponentInChildren<Slot>();
//     }
//     public void SetFollower(Sprite sprite, int quantity)
//     {
//         inventorySlot.SetSlot(sprite, quantity);
//     }
//     // Start is called before the first frame update
//     void Start()
//     {

//     }
//     void Update()
//     {
//         Vector2 position;
//         RectTransformUtility.ScreenPointToLocalPointInRectangle(
//             (RectTransform)_canvas.transform, // canvas position adjustments
//             Input.mousePosition,
//             _canvas.worldCamera,
//             out position);
//         transform.position = _canvas.transform.TransformPoint(position);
//     }
//     public void Toggle(bool isOn)
//     {
//         Debug.Log($"Item toggled {isOn}");
//         gameObject.SetActive(isOn);
//     }
// }
