using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private List<string> respawnItemIds = new List<string>();
    // Start is called before the first frame update

    private void Start()
    {
        Persistence.OnLoadGame += LoadCollectibles;
    }

    private void OnDisable()
    {
        Persistence.OnLoadGame -= LoadCollectibles;
    }

    private void LoadCollectibles(SaveData gameData)
    {
        ItemCollectible[] items = FindObjectsOfType<ItemCollectible>();
        List<string> itemIds = GetItemIds(items);
        GetRespawnItemIds(itemIds);

        // Spawn every item of id from the items to respawn list
        foreach (var itemId in respawnItemIds)
        {
            if (gameData.activeItems.TryGetValue(itemId, out ItemSaveData itemSaveData))
            {
                Debug.Log($"Instantiated {itemSaveData.inventoryItemData.ItemPrefab.name}");
                Instantiate(itemSaveData.inventoryItemData.ItemPrefab, itemSaveData.position, itemSaveData.rotation);
            }
        }
    }

    private void GetRespawnItemIds(List<string> itemIds)
    {
        // Find ids of items to respawn and put their ids into a list
        foreach (var item in SaveGameManager.gameData.activeItems)
        {
            if (!itemIds.Contains(item.Key))
            {
                respawnItemIds.Add(item.Key);
            }
        }
    }

    private static List<string> GetItemIds(ItemCollectible[] items)
    {
        // Find world Item collectibles and put their ids into a list
        List<string> itemIds = new List<string>();
        foreach (var item in items)
        {
            itemIds.Add(item.Id);
        }

        return itemIds;
    }
}
