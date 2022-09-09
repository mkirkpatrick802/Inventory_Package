using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropItem : Interactable
{

    public static DropItem SpawnItem(Vector2 pos, Item item, Transform dropItemPrefab)
    {
        Transform transform = Instantiate(dropItemPrefab, pos, Quaternion.identity);

        DropItem dropItem = transform.GetComponent<DropItem>();
        dropItem.SetItem(item);

        return dropItem;
    }

    private Item _item;
    private SpriteRenderer _sprite;
    private TextMeshPro _text;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _text = transform.Find("Amount").GetComponent<TextMeshPro>();
    }

    private void SetItem(Item item)
    {
        _item = item;
        _sprite.sprite = item.icon;
        transform.name = item.name;

        if (item.amount > 1)
        {
            _text.SetText(item.amount.ToString());
        }
        else
        {
            _text.SetText("");
        }
    }

    public override Item Interact()
    {
        Destroy(gameObject);
        return _item;
    }
}
