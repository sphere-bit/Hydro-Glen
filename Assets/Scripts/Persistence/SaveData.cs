public class SaveData
{
    public SerializableDict<string, ChestSaveData> chestDict;

    public SaveData()
    {
        // Save ChestInventory
        this.chestDict = new SerializableDict<string, ChestSaveData>();
    }
}