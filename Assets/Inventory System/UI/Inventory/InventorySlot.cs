using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static event Action<InventorySlot> InventorySlotDropped;

    public Item GetItem() => _item;
    private Item _item;

    private Inventory _inventory;

    private Transform _defaultParent;
    private Transform _inventoryParent;

    public Transform GetLastParent => _lastParent;
    private Transform _lastParent;

    private Item _tempItem; //When re-adding item to inventory use tempItem

    //Dragging
    private Canvas _canvas;
    private CanvasGroup _canvasGroup;
    private RectTransform _rectTransform;

    //Visuals
    private Image _image;
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _canvas = transform.root.GetComponent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _rectTransform = GetComponent<RectTransform>();
    }

    public void InventorySpawned(Inventory inventory, Item item, Transform inventoryParent, Transform defaultParent)
    {
        //Set Local Variables
        _inventory = inventory;
        _item = item;
        _defaultParent = defaultParent;
        _inventoryParent = inventoryParent;

        //Set Visuals
        _image = GetComponent<Image>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _image.sprite = item.icon;

        UpdateAmount();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _lastParent = transform.parent;
        transform.SetParent(_defaultParent);
        _canvasGroup.alpha = .6f;
        _canvasGroup.blocksRaycasts = false;
        RemoveItem();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
        Return();
    }

    private void Return()
    {
        if(transform.parent == _defaultParent)
        {
            transform.SetParent(_lastParent, false); 
            transform.localPosition = Vector3.zero;
        }

        if(transform.parent == _inventoryParent)
        {
            _inventory.AddItem(_tempItem);
        }

        InventorySlotDropped?.Invoke(this);
    }

    private void RemoveItem()
    {
        Item tempItem = Instantiate(_item);
        _inventory.RemoveItem(_item);
        _tempItem = tempItem;
    }

    public void UpdateAmount()
    {
        if (!_item.isStackable) return;
        if (_item.amount > 1)
        {
            _text.SetText(_item.amount.ToString());
        }
        else if(_item.amount == 1)
        {
            _text.SetText("");
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
