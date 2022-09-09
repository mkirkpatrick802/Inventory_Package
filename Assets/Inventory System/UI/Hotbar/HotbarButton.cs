using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HotbarButton : MonoBehaviour, IDropHandler
{
    public event Action<HotbarButton, InventorySlot> OnButtonClicked;

    private KeyCode _keyCode;
    private TMP_Text _text;
    private int _keyNumber;

    public Item GetItem => _assignedItem?.GetItem();
    private InventorySlot _assignedItem;

    public void SetAllowTypes(ItemType[] allowed) => _allowedTypes = allowed;
    private ItemType[] _allowedTypes;

    private void OnValidate()
    {
        _keyNumber = transform.GetSiblingIndex() + 1;
        _keyCode = KeyCode.Alpha0 + _keyNumber;

        if (_text == null)
            _text = GetComponentInChildren<TMP_Text>();

        _text.SetText(_keyNumber.ToString());
        gameObject.name = "Hotbar Button " + _keyNumber;
    }

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(HandleClick);
    }

    private void OnEnable()
    {
        InventorySlot.InventorySlotDropped += InventorySlotDropped;
    }

    private void OnDisable()
    {
        InventorySlot.InventorySlotDropped -= InventorySlotDropped;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_keyCode))
            HandleClick();
    }

    private void HandleClick()
    {
        OnButtonClicked?.Invoke(this, _assignedItem);
    }

    public void Selected(bool isSelected)
    {
        if (isSelected)
        {
            transform.GetComponent<Image>().color = Color.red;
        }
        else
        {
            transform.GetComponent<Image>().color = Color.white;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (_assignedItem != null) return;

        InventorySlot slot = eventData.pointerDrag.GetComponent<InventorySlot>();
        if (slot == null) return;

        bool match = false;
        foreach (var allowedType in _allowedTypes)
            if (allowedType == slot.GetItem().itemType) { match = true; break; }

        if (match == false) return;
        slot.transform.SetParent(transform, false);
        slot.transform.localPosition = Vector3.zero;

        _text.gameObject.SetActive(false);
        _assignedItem = slot;
    }

    private void InventorySlotDropped(InventorySlot slot)
    {
        if (_assignedItem == null) return;
        if (slot.transform.parent == transform) return;
        if (slot.GetLastParent != transform) return;

        _text.gameObject.SetActive(true);
        _assignedItem = null;
    }
}
