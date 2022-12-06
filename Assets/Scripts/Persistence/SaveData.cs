using System.Collections.Generic;

public class SaveData
{
    public SerializableDict<string, ChestSaveData> chestDict;
    public List<string> collectedItems;

    public SaveData()
    {
        collectedItems = new List<string>();

        // Save ChestInventory
        chestDict = new SerializableDict<string, ChestSaveData>();
    }
}