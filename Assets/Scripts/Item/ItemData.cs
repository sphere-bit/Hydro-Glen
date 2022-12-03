using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item Data")]
public class ItemData : ScriptableObject
{
    // Data store for each item
    public int id;
    public string displayName;
    [TextArea(4, 4)]
    public string details;
    public Sprite icon;
    public int maxStackSize;

    public int Id => id;
    public string DisplayName => displayName;
    public string Details => details;
    public Sprite Icon => icon;
    public int MaxStackSize => maxStackSize;

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
        return "[id] " + id.ToString() + " [displayName] " + displayName.ToString() + " [details] " + details.ToString() + " [icon] " + icon.ToString() + " [maxStackSize] " + maxStackSize.ToString();
    }
}
