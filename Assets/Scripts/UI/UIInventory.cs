using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField]
    private UIItemSlot _itemSlot;
    [SerializeField]
    private RectTransform _contentPanel;

    private List<UIItemSlot> _itemSlots = new List<UIItemSlot>();

    public void initUIInventory(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            UIItemSlot uiItem = Instantiate(_itemSlot, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(_contentPanel);
            _itemSlots.Add(uiItem);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.SetActive(false);
    }
}
