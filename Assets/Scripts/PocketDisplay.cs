using UnityEngine;

public class PocketDisplay : StaticInventoryDisplay
{
    private int maxIndexSize = 12;
    private int currSlotIndex = 0;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        currSlotIndex = 0;
        maxIndexSize = slots.Length - 1;

        slots[currSlotIndex].ToggleHighlight();
    }
    protected override void OnEnable()
    {
        base.OnEnable(); // subscribe to inventory display refresh
    }
    protected override void OnDisable()
    {

    }
    // Update is called once per frame
    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            // scroll up; move left.
            if (scroll > 0f)
            {
                ChangeIndex(-1);
                return;
            }
            else // scroll down; move right.
            {
                ChangeIndex(1);
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetIndex(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetIndex(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetIndex(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetIndex(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetIndex(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SetIndex(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SetIndex(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SetIndex(7);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SetIndex(8);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SetIndex(9);
        }
        else if (Input.GetKeyDown(KeyCode.Minus))
        {
            SetIndex(10);
        }
        else if (Input.GetKeyDown(KeyCode.Equals))
        {
            SetIndex(11);
        }

        if (Input.GetMouseButton(1))
        {
            UseItem();
        }
    }

    private void UseItem()
    {
        // Access slot item
        if (slots[currSlotIndex].AssignedSlot.ItemData != null)
        {
            slots[currSlotIndex].AssignedSlot.ItemData.UseItem();
            Debug.Log(slots[currSlotIndex].AssignedSlot.ItemData);
        }
    }

    private void ChangeIndex(int direction)
    {
        // Call from update to move left or right a slot selection
        slots[currSlotIndex].ToggleHighlight();
        currSlotIndex += direction;

        // selection wrap around
        if (currSlotIndex > maxIndexSize)
        {
            // At end, go back to 0
            currSlotIndex = 0;
        }
        if (currSlotIndex < 0)
        {
            // At beginning, move to end
            currSlotIndex = maxIndexSize;
        }

        slots[currSlotIndex].ToggleHighlight();
    }

    private void SetIndex(int newIndex)
    {
        slots[currSlotIndex].ToggleHighlight();

        // wrap around
        if (newIndex < 0)
        {
            currSlotIndex = 0;
        }
        if (newIndex > maxIndexSize)
        {
            newIndex = maxIndexSize;
        }

        currSlotIndex = newIndex;
        slots[currSlotIndex].ToggleHighlight();

    }
}
