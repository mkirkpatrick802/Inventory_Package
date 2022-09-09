using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewConsumable", menuName = "Items/Consumable Item", order = 0)]
public class Consumable : Item
{
    [Header("Use Controller")]
    public UseController useController;
}
