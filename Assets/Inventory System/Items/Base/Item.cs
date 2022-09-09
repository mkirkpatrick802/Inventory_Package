using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Items/General Item", order = 0)]
public class Item : ScriptableObject
{
    [Header("General Settings")]
    public new string name;
    public Sprite icon;
    public ItemType itemType;
    public bool isStackable;
    public int amount;
}


/// <summary>
/// If you wish to add different item types add them to this ENUM and create a script with the same name that derives from this script. 
/// </summary>
public enum ItemType
{
    Null,
    Currency,
    Consumable,
    SpellComponent,
    Spell
}
