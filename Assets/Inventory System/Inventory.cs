using System;
using System.Collections.Generic;

public class Inventory
{
    public event Action OnItemListChanged;
    public List<Item> ItemList => _itemList;
    private List<Item> _itemList;

    public int maxSize;

    public Inventory(int maxSize)
    {
        this.maxSize = maxSize;
        _itemList = new List<Item>();
    }

    public void AddItem(Item item)
    {
        if (item.isStackable)
        {
            bool itemAlreadyInInventory = false;
            foreach (Item inventoryItem in _itemList)
            {
                if (inventoryItem.name != item.name) continue;

                inventoryItem.amount += item.amount;
                itemAlreadyInInventory = true;
                break;
            }

            if (!itemAlreadyInInventory) _itemList.Add(item);
        }
        else
        {
            _itemList.Add(item);
        }

        OnItemListChanged?.Invoke();
    }

    public void RemoveItem(Item item)
    {
        _itemList.Remove(item);
        OnItemListChanged?.Invoke();
    }
}
