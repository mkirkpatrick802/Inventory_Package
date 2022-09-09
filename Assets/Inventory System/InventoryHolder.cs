using UnityEngine;

public class InventoryHolder : MonoBehaviour
{
    [Header("Inventory Settings")]
    [SerializeField] private int _inventorySize;
    [SerializeField] private GameObject _inventoryMenu;

    private Inventory _inventory;

    private void Awake()
    {
        _inventory = new Inventory(_inventorySize);
    }

    private void Start()
    {
        _inventoryMenu = Instantiate(_inventoryMenu);
        _inventoryMenu.GetComponentInChildren<InventoryUI>().SetInventoryUI(_inventory);
        _inventoryMenu.gameObject.SetActive(false);
    }

    public void AddItem(Item addItem)
    {
        _inventory.AddItem(addItem);
        //print(_inventory.ItemList);
    }

    //Allow Access to The Inventory UI

    public void SetInventoryActive(bool isActive) => _inventoryMenu.SetActive(isActive);

    public bool InventoryActive => _inventoryMenu.activeSelf;
}
