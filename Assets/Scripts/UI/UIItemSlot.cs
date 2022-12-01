using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIItemSlot : MonoBehaviour
{
    [SerializeField]
    private Image _itemImage;
    [SerializeField]
    private TMP_Text _quantityText;
    [SerializeField]
    private Image _borderImage;
    public event Action<UIItemSlot> OnItemDrop;
    public event Action<UIItemSlot> OnItemBeginDrag, OnItemEndDrag;
    public event Action<UIItemSlot> OnItemRightPointerClick, OnItemLeftPointerClick;
    private bool _isItemSlotEmpty = true;

    public void Awake()
    {
        ClearSlot();
        DeselectSlot();
    }
    private void SelectSlot()
    {
        _borderImage.enabled = true;
    }
    private void DeselectSlot()
    {
        _borderImage.enabled = false;
    }

    public void SetSlot(Sprite itemSprite, int itemQuantity)
    {
        _itemImage.gameObject.SetActive(true);
        _itemImage.sprite = itemSprite;
        _quantityText.text = itemQuantity.ToString();
        _isItemSlotEmpty = false;
    }
    private void ClearSlot()
    {
        _itemImage.gameObject.SetActive(false);
        _isItemSlotEmpty = true;
    }
    public void OnDrop()
    {
        OnItemDrop?.Invoke(this);
    }
    public void OnBeginDrag()
    {
        if (_isItemSlotEmpty)
        {
            return;
        }
        OnItemBeginDrag?.Invoke(this);
    }
    public void OnEndDrag()
    {
        OnItemEndDrag?.Invoke(this);
    }
    public void OnPointerClick(BaseEventData data)
    {
        if (_isItemSlotEmpty)
        {
            return;
        }
        // Check if using LMB or RMB.
        PointerEventData pointerData = (PointerEventData)data;
        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            OnItemRightPointerClick?.Invoke(this);
        }
        else
        {
            OnItemLeftPointerClick?.Invoke(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
