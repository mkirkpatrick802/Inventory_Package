using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [Header("World Interaction Setting")]
    [SerializeField][Range(.5f, 2f)] private float _interactionRange;
    [SerializeField] private LayerMask _interactionLayer;

    [Header("Item Interaction Settings")]
    [SerializeField] private Hotbar _hotbar;

    private InventoryHolder _inventory;

    private void Awake()
    {
        _inventory = GetComponent<InventoryHolder>();
        if (_inventory == null) Debug.LogWarning("Inventory Holder script not added to " + gameObject.name);
    }

    //Interacts with game objects in the scene
    public void Interact()
    {
        Collider2D interactableCol = Physics2D.OverlapCircle(transform.position, _interactionRange, _interactionLayer);
        if (interactableCol == null) return;
        Interactable interactable = interactableCol.gameObject.GetComponent<Interactable>();
        Item item;
        if ((item = interactable.Interact()) != null) _inventory.AddItem(item);
    }

    //Use the selected Item in the hotbar
    public void UseSelectedItem()
    {
        if (_hotbar.GetSelectedItem() == null) return;

        Item selectedItem = _hotbar.GetSelectedItem();
        switch (selectedItem.itemType)
        {
            case ItemType.Spell:
                Spell selectedSpell = (Spell)selectedItem;
                selectedSpell.castController.Use();
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _interactionRange);
    }
}
