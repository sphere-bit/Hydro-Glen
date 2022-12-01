using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemDescription : MonoBehaviour
{
    [SerializeField]
    private Image _itemImage;
    [SerializeField]
    private TMP_Text _itemTitle;
    [SerializeField]
    private TMP_Text _itemDescription;

    public void Awake()
    {
        ClearDescription();
    }

    public void ClearDescription()
    {
        _itemImage.gameObject.SetActive(false);
        _itemTitle.text = "";
        _itemDescription.text = "";
    }
    public void SetDescription(Sprite sprite, string itemName, string itemDescription)
    {
        _itemImage.gameObject.SetActive(true);
        _itemImage.sprite = sprite;
        _itemTitle.text = itemName;
        _itemDescription.text = itemDescription;
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
