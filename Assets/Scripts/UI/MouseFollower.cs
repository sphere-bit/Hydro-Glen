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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
