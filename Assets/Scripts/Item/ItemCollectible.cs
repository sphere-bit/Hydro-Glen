#pragma warning disable 108

using System;
using UnityEngine;

// To enable holding
[RequireComponent(typeof(Rigidbody2D))]
// To detect collision with obstacles
[RequireComponent(typeof(CircleCollider2D))]
// To enable item data to persist
[RequireComponent(typeof(Uid))]
[Serializable]
public class ItemCollectible : MonoBehaviour
{
    public float pickUpRadius = 1f;
    public ItemData itemData;

    private CircleCollider2D collider;
    private Rigidbody2D rb;
    private string id;
    public string Id => id;

    public ItemData ItemData => itemData;

    private void Awake()
    {
        Persistence.OnLoadGame += LoadCollectible;
    }

    private void Start()
    {
        ItemSaveData itemSaveData = new ItemSaveData(itemData, transform.position, transform.rotation);
        SaveGameManager.gameData.activeItems.Add(key: GetComponent<Uid>().Id, itemSaveData);
    }

    public void LoadCollectible(SaveData gameData)
    {
        if (gameData.activeItems.TryGetValue(GetComponent<Uid>().Id, out ItemSaveData itemSaveData))
        {
            this.itemData = itemSaveData.inventoryItemData;
            this.transform.position = itemSaveData.position;
            this.transform.rotation = itemSaveData.rotation;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }


    private void OnDestroy()
    {
        if (SaveGameManager.gameData.activeItems.ContainsKey(GetComponent<Uid>().Id))
        {
            SaveGameManager.gameData.activeItems.Remove(GetComponent<Uid>().Id);
        }
        Persistence.OnLoadGame -= LoadCollectible;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            AdjustItemPosition();
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            AdjustItemPosition();
        }
    }

    private void AdjustItemPosition()
    {
        // Ensure the item is not placed on an obstacle
        // GameObject obstacles = GameObject.FindGameObjectWithTag("Obstacle");

        Player player = FindObjectOfType<Player>();
        Vector2 freePosition = player.transform.position;
        this.transform.position = freePosition;
        this.transform.parent = null;
        return;
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
