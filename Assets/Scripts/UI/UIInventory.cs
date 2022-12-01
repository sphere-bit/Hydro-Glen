using System;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField]
    private UIItemSlot _itemSlot;
    [SerializeField]
    private RectTransform _contentPanel;
    [SerializeField]
    private UIItemDescription _itemDescription;
    private List<UIItemSlot> _itemSlots = new List<UIItemSlot>();

    public Sprite sprite;
    public int quantity;
    public string title, description;

    private void Awake()
    {
        _itemDescription.ClearDescription();
    }

    public void initUIInventory(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            UIItemSlot uiItem = Instantiate(_itemSlot, Vector3.zero, Quaternion.identity);
            uiItem.gameObject.SetActive(true);
            uiItem.transform.SetParent(_contentPanel);
            _itemSlots.Add(uiItem);
            uiItem.OnItemLeftPointerClick += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnItemDrop += HandleSwap;
            uiItem.OnItemRightPointerClick += HandleShowItemActions;
        }
    }

    private void HandleShowItemActions(UIItemSlot obj)
    {
        throw new NotImplementedException("HandleShowItemActions");
    }

    private void HandleEndDrag(UIItemSlot obj)
    {
        throw new NotImplementedException("HandleEndDrag");
    }

    private void HandleSwap(UIItemSlot obj)
    {
        throw new NotImplementedException("HandleSwap");
    }

    private void HandleBeginDrag(UIItemSlot obj)
    {
        throw new NotImplementedException("HandleBeginDrag");
    }

    private void HandleItemSelection(UIItemSlot obj)
    {
        _itemDescription.SetDescription(sprite, title, description);
        _itemSlots[0].SelectSlot();
        Debug.Log(obj.name + " clicked!");
    }

    // Start is called before the first frame update
    void Start()
    {
        _itemDescription.ClearDescription();
        _itemSlots[0].SetSlot(sprite, quantity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
