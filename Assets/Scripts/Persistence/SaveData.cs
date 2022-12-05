public class SaveData
{
    public SerializableDict<string, ChestSaveData> chestDict;

    public SaveData()
    {
        // Save ChestInventory
        chestDict = new SerializableDict<string, ChestSaveData>();
    }
}