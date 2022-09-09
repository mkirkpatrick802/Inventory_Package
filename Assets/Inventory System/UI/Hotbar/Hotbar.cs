using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    [Header("Hotbar Settings")]
    [SerializeField] private ItemType[] _allowedItemTypes;

    private HotbarButton _selectedSlot;

    private void Awake()
    {
        foreach (HotbarButton button in GetComponentsInChildren<HotbarButton>())
        {
            button.OnButtonClicked += ButtonClicked;
            button.SetAllowTypes(_allowedItemTypes);
        }
    }

    private void ButtonClicked(HotbarButton hotbarSlot, InventorySlot itemSlot)
    {
        if (itemSlot != null)
        {
            Item item = itemSlot.GetItem();
            switch (item.itemType)
            {
                //Hotbar Slot is pressed and the corresponding item should be used. 
                case ItemType.Consumable:
                    Consumable consumable = (Consumable)item;
                    consumable.useController.Use();
                    consumable.amount--;
                    itemSlot.UpdateAmount();
                    break;
                default:
                    SelectedHotbar(hotbarSlot);
                    break;
            }
        }
        else
        {
            SelectedHotbar(hotbarSlot);
        }
    }

    //Hotbar Slot Selected
    private void SelectedHotbar(HotbarButton hotbarSlot)
    {
        foreach (HotbarButton button in GetComponentsInChildren<HotbarButton>())
        {
            button.Selected(false);
        }

        _selectedSlot = hotbarSlot;
        hotbarSlot.Selected(true);
    }

    public Item GetSelectedItem()
    {
        if(_selectedSlot == null) return null;
        return _selectedSlot.GetItem;
    }
}
