using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item Database")]
public class Database : ScriptableObject
{
    [SerializeField] private List<ItemData> itemDatabase;

    [ContextMenu("Set ids")]
    public void SetItemIds()
    {
        itemDatabase = new List<ItemData>();

        // Load all item data scriptable objects into a folder called ItemData
        // then sort by id and put into a list called foundItems
        var foundItems = Resources.LoadAll<ItemData>("ItemData").OrderBy(i => i.Id).ToList();

        var hasIdInRange = foundItems.Where(i => i.Id != -1 && i.Id < foundItems.Count).OrderBy(i => i.Id).ToList();
        var hasIdNotInRange = foundItems.Where(i => i.Id != -1 && i.Id >= foundItems.Count).OrderBy(i => i.Id).ToList();
        var hasNoId = foundItems.Where(i => i.Id <= -1).ToList();

        var index = 0;
        for (int i = 0; i < foundItems.Count; i++)
        {
            ItemData itemToAdd;
            itemToAdd = hasIdInRange.Find(data => data.Id == i);

            if (itemToAdd != null)
            {
                itemDatabase.Add(itemToAdd);
            }
            else if (index < hasNoId.Count)
            {
                hasNoId[index].Id = i;
                itemToAdd = hasNoId[index];
                index++;
                itemDatabase.Add(itemToAdd);
            }
#if UNITY_EDITOR
            if (itemToAdd)
            {
                EditorUtility.SetDirty(itemToAdd);
            }
#endif
        }

        foreach (var item in hasIdNotInRange)
        {
            // Allow new additions of items
            itemDatabase.Add(item);
#if UNITY_EDITOR
            if (item)
            {
                EditorUtility.SetDirty(item);
            }
#endif
        }
#if UNITY_EDITOR
        AssetDatabase.SaveAssets();
#endif
    }

    public ItemData GetItem(int id)
    {
        return itemDatabase.Find(i => i.Id == id);
    }
}
