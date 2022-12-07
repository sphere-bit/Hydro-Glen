using System.Collections.Generic;

public class SaveData
{
    // Data structures for storing serializable dictionaries and writing them to JSON.
    public SerializableDict<string, ChestSaveData> chestDict;
    public SerializableDict<string, ItemSaveData> activeItems;
    public List<string> collectedItemIds;
    public InventorySaveData playerInventory;

    public SaveData()
    {
        playerInventory = new InventorySaveData();
        collectedItemIds = new List<string>();
        // Save collectible items
        activeItems = new SerializableDict<string, ItemSaveData>();
        // Save ChestInventory
        chestDict = new SerializableDict<string, ChestSaveData>();
    }
}