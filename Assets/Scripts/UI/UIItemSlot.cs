using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIItemSlot : MonoBehaviour
{
    [SerializeField]
    private Image _slotImage;
    [SerializeField]
    private TMP_Text _slotQuantity;
    [SerializeField]
    private Image _selectedImage;
    public event Action<UIItemSlot> OnItemDrop;
    public event Action<UIItemSlot> OnItemBeginDrag, OnItemEndDrag;
    public event Action<UIItemSlot> OnItemRightPointerClick, OnItemLeftPointerClick;
    private bool _isItemSlotEmpty = true;

    public void Awake()
    {
        ClearSlot();
        DeselectSlot();
    }
    public void SelectSlot()
    {
        _selectedImage.enabled = true;
    }
    public void DeselectSlot()
    {
        _selectedImage.enabled = false;
    }

    public void SetSlot(Sprite itemSprite, int itemQuantity)
    {
        _slotImage.gameObject.SetActive(true);
        _slotImage.sprite = itemSprite;
        _slotQuantity.text = itemQuantity.ToString();
        _isItemSlotEmpty = false;
    }
    private void ClearSlot()
    {
        _slotImage.gameObject.SetActive(false);
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
