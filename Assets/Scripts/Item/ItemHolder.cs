using System.Collections;
using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(Rigidbody2D))]
public class ItemHolder : MonoBehaviour
{
    public Transform heldPosition;
    public LayerMask pickUpMask;
    public Vector3 Direction { get; set; } // drop direction
    private ItemData itemData;
    private GameObject itemHeld;
    [SerializeField] private float _throwSpeed;
    [SerializeField] private float _throwDistance;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (itemHeld)
            {
                // Drop item infront of the player and subtract (for at their feet).
                itemHeld.transform.position = transform.position + new Vector3(0, -.5f, 0) + Direction;
                // Remove item from player
                itemHeld.transform.parent = null;
                if (itemHeld.GetComponent<Rigidbody2D>())
                    itemHeld.GetComponent<Rigidbody2D>().simulated = true;
                // Toggle itemHeld.
                itemHeld = null;
            }
            else
            {
                // Define the item that is picked up.
                Collider2D pickUpItem = Physics2D.OverlapCircle(transform.position + Direction, .4f, pickUpMask);
                print(pickUpItem.gameObject);

                if (pickUpItem)
                {
                    itemHeld = pickUpItem.gameObject;
                    itemHeld.transform.position = heldPosition.position;
                    // Make item follow player
                    itemHeld.transform.parent = transform;
                    if (itemHeld.GetComponent<Rigidbody2D>())
                    {
                        itemHeld.GetComponent<Rigidbody2D>().simulated = false;
                    }
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if (itemHeld)
            {
                StartCoroutine(ThrowItem(itemHeld));
                itemHeld = null;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            if (itemHeld)
            {
                StartCoroutine(StashItem(itemHeld));
                itemHeld = null;
            }
        }
    }

    IEnumerator StashItem(GameObject item)
    {
        var inventory = transform.GetComponent<InventoryHolder>();
        if (!inventory)
        {
            yield return null;
        }

        ItemCollectible collectible = item.GetComponent<ItemCollectible>();
        print(collectible);
        itemData = collectible.ItemData;

        if (inventory.Inventory.hasItemToAdd(itemData, 1))
        {
            print("itemData of " + collectible + " " + itemData);
            Destroy(item);
            yield return null;
        }
    }

    IEnumerator ThrowItem(GameObject item)
    {
        Vector3 startPos = item.transform.position;
        Vector3 endPos = transform.position + Direction * _throwDistance + new Vector3(0, -.5f, 0);
        // Detach item from player
        item.transform.parent = null;

        // To allow animation to fully play
        for (int i = 0; i < 25; i++)
        {
            item.transform.position = Vector3.Lerp(startPos, endPos, i * _throwSpeed);
            yield return null;
        }
        if (item.GetComponent<Rigidbody2D>())
            item.GetComponent<Rigidbody2D>().simulated = true;
    }
}