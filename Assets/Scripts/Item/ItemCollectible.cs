#pragma warning disable 108

using System;
using UnityEngine;

// To enable holding
// [RequireComponent(typeof(Rigidbody2D))]
// To detect collision with obstacles
[RequireComponent(typeof(CircleCollider2D))]
// To enable item data to persist
[RequireComponent(typeof(Uid))]
[Serializable]
public class ItemCollectible : MonoBehaviour
{
    public ItemData itemData;
    private CircleCollider2D collider;
    private bool onTrigger = false;
    // private Rigidbody2D rb;
    private string id;
    private PlayerInventoryHolder inventoryTarget;
    private Vector3 playerPosition;
    [SerializeField] private float collectSpeed;

    public string Id => id;

    public ItemData ItemData => itemData;

    private void Awake()
    {
        Persistence.OnLoadGame += LoadCollectible;
    }

    private ItemCollectible GetItemOfId(string id)
    {
        if (id == this.id)
        {
            return this;
        }
        return null;
    }

    private void Start()
    {
        id = GetComponent<Uid>().Id;
        ItemSaveData itemSaveData = new ItemSaveData(itemData, transform.position, transform.rotation);

        // Add item to active items list
        if (!SaveGameManager.gameData.activeItems.ContainsKey(id))
        {
            SaveGameManager.gameData.activeItems.Add(id, itemSaveData);
        }
    }

    public void LoadCollectible(SaveData gameData)
    {
        // gameData is an overload parameter that matches UnityAction<SaveData>
        if (gameData.collectedItemIds.Contains(item: id))
        {
            Debug.Log($"Destroyed {this.gameObject.name}");
            Destroy(this.gameObject);
        }

        // if (gameData.activeItems.TryGetValue(GetComponent<Uid>().Id, out ItemSaveData itemSaveData))
        // {
        //     this.itemData = itemSaveData.inventoryItemData;
        //     this.transform.position = itemSaveData.position;
        //     this.transform.rotation = itemSaveData.rotation;
        // }
        // else
        // {
        //     Destroy(this.gameObject);
        // }
    }


    private void OnDestroy()
    {
        if (SaveGameManager.gameData.activeItems.ContainsKey(id))
        {
            SaveGameManager.gameData.activeItems.Remove(id);
        }
        Persistence.OnLoadGame -= LoadCollectible;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inventoryTarget = other.transform.GetComponent<PlayerInventoryHolder>();
            playerPosition = other.transform.position;
            onTrigger = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerPosition = other.transform.position;

            if (!onTrigger && inventoryTarget.HasItemToAdd(itemData, 1))
            {
                onTrigger = true;
                transform.localScale = Vector2.MoveTowards(transform.localScale, new Vector2(.5f, .5f), Time.deltaTime * collectSpeed);
                SaveGameManager.gameData.collectedItemIds.Add(id);
                Destroy(gameObject);
            }
            else
            {
                transform.localScale = new Vector2(1f, 1f);
            }
        }
    }

    void Update()
    {
        if (!onTrigger)
        {
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, playerPosition, Time.deltaTime * collectSpeed);
        transform.localScale = Vector2.MoveTowards(transform.localScale, new Vector2(.5f, .5f), Time.deltaTime * collectSpeed);

        if (Vector2.Distance(transform.position, playerPosition) <= 1f)
        {
            if (!inventoryTarget)
            {
                return;
            }

            if (inventoryTarget.HasItemToAdd(itemData, 1))
            {
                SaveGameManager.gameData.collectedItemIds.Add(id);
                Destroy(gameObject);
            }
            else
            {
                transform.localScale = new Vector2(1f, 1f);
                // stop item from following player
                onTrigger = false;
            }
        }
    }
}

[Serializable]
public struct ItemSaveData
{
    public ItemData inventoryItemData;
    public Vector2 position;
    public Quaternion rotation;

    public ItemSaveData(ItemData _inventoryItemData, Vector2 _position, Quaternion _rotation)
    {
        inventoryItemData = _inventoryItemData;
        position = _position;
        rotation = _rotation;
    }
}
