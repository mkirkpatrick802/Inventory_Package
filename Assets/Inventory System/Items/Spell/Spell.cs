using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpell", menuName = "Items/Spell", order = 0)]
public class Spell : Item
{
    [Header("Spell Settings")]
    public float duration;
    public float cooldown;
    public int damage;

    [Header("Recipe Settings")]
    public SpellComponent[] recipe;

    [Header("Cast Settings")]
    public UseController castController;
}
