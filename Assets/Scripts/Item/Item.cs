using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Item")]
public class Item : ScriptableObject
{
    public int id;
    public string displayName;
    [TextArea(4, 4)]
    public string details;
    public Sprite icon;
    public int maxStackSize;
}
