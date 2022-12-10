using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item Data")]
public class ItemData : ScriptableObject
{
    // Data store for each item. An SO (Scriptable object)
    // It could be inherited to have branched versions of items, eg. potions and equipment.

    public int Id = -1;
    public string DisplayName;
    [TextArea(4, 4)]
    public string Details;
    public Sprite Icon;
    public int MaxStackSize;
    public int sellValue;
    public ItemCollectible ItemPrefab;

    public override bool Equals(object other)
    {
        return base.Equals(other);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return "[id] " + Id.ToString() + " [displayName] " + DisplayName.ToString() + " [details] " + Details.ToString() + " [icon] " + Icon.ToString() + " [maxStackSize] " + MaxStackSize.ToString();
    }
}
