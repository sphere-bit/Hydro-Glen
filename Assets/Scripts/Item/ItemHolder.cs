// using System;
// using System.Collections;
// using UnityEngine;

// [Serializable]
// [RequireComponent(typeof(Rigidbody2D))]
// public class ItemHolder : MonoBehaviour
// {
//     [SerializeField] private float _throwSpeed;
//     [SerializeField] private float _throwDistance;

//     public Transform heldPosition;
//     public LayerMask pickUpMask;
//     public Vector3 Direction { get; set; } // drop direction

//     private ItemData itemData;
//     private GameObject itemHeld;
//     private Rigidbody2D rb;
//     private Vector3 lastPlayerPosition;
//     public Vector3 LastPlayerPosition => LastPlayerPosition;

//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.E))
//         {
//             if (itemHeld)
//             {
//                 DropItem();
//                 lastPlayerPosition = transform.position;
//             }
//             else
//             {
//                 PickUpItem();
//             }
//         }
//         else if (Input.GetKeyDown(KeyCode.Q))
//         {
//             if (itemHeld)
//             {
//                 StartCoroutine(ThrowItem(itemHeld));
//                 itemHeld = null;
//                 lastPlayerPosition = transform.position;
//             }
//         }
//         else if (Input.GetKeyDown(KeyCode.Z))
//         {
//             if (itemHeld)
//             {
//                 StartCoroutine(StashItem(itemHeld));
//                 itemHeld = null;
//             }
//         }
//     }

//     private void PickUpItem()
//     {
//         // Define the item that is picked up.
//         Collider2D pickUpItem = Physics2D.OverlapCircle(transform.position + Direction, .4f, pickUpMask);

//         if (pickUpItem)
//         {
//             itemHeld = pickUpItem.gameObject;
//             itemHeld.transform.position = heldPosition.position;
//             // Make item follow player
//             itemHeld.transform.parent = transform;

//             rb = itemHeld.GetComponent<Rigidbody2D>();
//             if (rb)
//             {
//                 rb.simulated = false;
//             }
//         }
//     }

//     private void DropItem()
//     {
//         // Drop item infront of the player and subtract (for at their feet).
//         itemHeld.transform.position = transform.position + new Vector3(0, -.5f, 0) + Direction;
//         // Remove item from player
//         itemHeld.transform.parent = null;

//         rb = itemHeld.GetComponent<Rigidbody2D>();
//         if (rb)
//             rb.simulated = true;
//         // Toggle itemHeld.
//         itemHeld = null;
//     }

//     IEnumerator StashItem(GameObject item)
//     {
//         ItemCollectible collectible = item.GetComponent<ItemCollectible>();
//         itemData = collectible.ItemData;

//         var inventory = transform.GetComponent<PlayerInventoryHolder>();

//         if (!inventory)
//         {
//             yield return null;
//         }


//         if (inventory.HasItemToAdd(itemData, 1))
//         {
//             SaveGameManager.gameData.collectedItemIds.Add(collectible.Id);
//             Destroy(collectible.gameObject);
//             yield return null;
//         }
//         else
//         {
//             Debug.Log("Inventory's full!");
//             DropItem();
//             yield return null;
//         }
//     }

//     IEnumerator ThrowItem(GameObject item)
//     {
//         Vector3 startPos = item.transform.position;
//         Vector3 endPos = transform.position + Direction * _throwDistance + new Vector3(0, -.5f, 0);

//         rb = item.GetComponent<Rigidbody2D>();
//         // Detach item from player
//         item.transform.parent = null;

//         // To allow animation to fully play
//         for (int i = 0; i < 25; i++)
//         {
//             item.transform.position = Vector3.Lerp(startPos, endPos, i * _throwSpeed);
//             yield return null;
//         }

//         if (rb)
//         {
//             rb.simulated = true;
//         }

//         yield return null;
//     }
// }