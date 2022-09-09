using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private Rigidbody2D _rb;
    private InventoryHolder _inventory;
    private InteractionController _interactionController;
    private Vector2 _moveAxis;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _inventory = GetComponent<InventoryHolder>();
        _interactionController = GetComponent<InteractionController>();
    }

    private void Update()
    {
        _moveAxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.E))
        {
            _interactionController.Interact();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _inventory.SetInventoryActive(!_inventory.InventoryActive);
        }

        if (Input.GetMouseButtonDown(0))
        {
            _interactionController.UseSelectedItem();
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = _moveAxis.normalized * _moveSpeed * Time.deltaTime;
    }
}
