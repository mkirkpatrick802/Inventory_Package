using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour, IDropHandler
{
    private Inventory _playerInventory;
    [SerializeField] Transform _itemSlotContainer;
    [SerializeField] Transform _itemSlotTemplate;

    public void SetInventoryUI(Inventory playerInventory)
    {
        _playerInventory = playerInventory;
        _playerInventory.OnItemListChanged += RefreshInventory;
        RefreshInventory();
    }
    private void RefreshInventory()
    {
        if (_itemSlotContainer == null) return;
        foreach (Transform child in _itemSlotContainer)
        {
            if (child == _itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (Item item in _playerInventory.ItemList)
        {
            RectTransform itemSlotTransform = Instantiate(_itemSlotTemplate, _itemSlotContainer).GetComponent<RectTransform>();
            itemSlotTransform.gameObject.SetActive(true);

            itemSlotTransform.GetComponent<InventorySlot>().InventorySpawned(_playerInventory, item, _itemSlotContainer ,transform);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.transform.SetParent(_itemSlotContainer);
    }
}
