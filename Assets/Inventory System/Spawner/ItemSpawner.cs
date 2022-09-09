using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Item To Spawn")]
    [SerializeField] private Transform _dropItemPrefab;
    [SerializeField] private Item _item;
    [SerializeField] private int _amount;

    private SpriteRenderer _sprite;

    private void OnValidate()
    {
        if (_item != null)
        {
            gameObject.name = _item.name + " Spawner";
        }
    }


    private void Start()
    {
        Item itemToSpawn = Instantiate(_item);
        if (itemToSpawn.isStackable)
        {
            itemToSpawn.amount = _amount;
        }
        DropItem.SpawnItem(transform.position, itemToSpawn, _dropItemPrefab);
        Destroy(gameObject);
    }
}
