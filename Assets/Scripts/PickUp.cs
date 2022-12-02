using System.Collections;
using UnityEngine;

[System.Serializable]
public class PickUp : MonoBehaviour
{
    public Transform holdPos;
    public LayerMask pickUpMask;
    public Vector3 Direction { get; set; } // drop direction
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
                Collider2D pickUpItem = Physics2D.OverlapCircle(transform.position + Direction, .4f, pickUpMask);
                if (pickUpItem)
                {
                    itemHeld = pickUpItem.gameObject;
                    itemHeld.transform.position = holdPos.position;
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
    }

    IEnumerator ThrowItem(GameObject item)
    {
        Vector3 startPos = item.transform.position;
        Vector3 endPos = transform.position + Direction * _throwDistance + new Vector3(0, -.5f, 0);
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