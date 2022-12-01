using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    [SerializeField]
    private Canvas _canvas;

    [SerializeField]
    private UIItemSlot _itemSlot;
    public void Awake()
    {
        _canvas = transform.root.GetComponent<Canvas>();
        _itemSlot = GetComponentInChildren<UIItemSlot>();
    }
    public void SetFollower(Sprite sprite, int quantity)
    {
        // Let 
        _itemSlot.SetSlot(sprite, quantity);
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)_canvas.transform, // canvas position adjustments
            Input.mousePosition,
            _canvas.worldCamera,
            out position);
        transform.position = _canvas.transform.TransformPoint(position);
    }
    public void Toggle(bool isOn)
    {
        Debug.Log($"Item toggled {isOn}");
        gameObject.SetActive(isOn);
    }
}
