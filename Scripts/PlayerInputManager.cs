using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    private SelectionManager _selectionManager;

    [SerializeField] private Camera _camera;
    [SerializeField] private RectTransform _selectionbox;
    [SerializeField] private LayerMask _unitLayerMask;
    [SerializeField] private LayerMask _floorLayerMask;

    private PlayerInput _inputControls;
    private Vector2 _startMousePosition;
    private float _mouseDownTime;
    private float _dragDelay = 0.1f;
    private bool _selectionInProgress = false;

    private void Awake()
    {
        _selectionManager = SelectionManager.Instance;
        _inputControls = new PlayerInput();
        _inputControls.MouseObjects.MouseLeft.started += ctx => OnStartSelection();
        _inputControls.MouseObjects.MouseLeft.canceled += ctx => OnEndSelection();
        _inputControls.MouseObjects.MouseRight.performed += ctx => SelectedMovementHandler();
    }

    private void OnEnable()
    {
        _inputControls.Enable();
    }

    private void OnDisable()
    {
        _inputControls.Disable();
    }

    private void Update()
    {
        if (_selectionInProgress)
        {
            ResizeSelectionBox();
        }
    }

    private void OnEndSelection()
    {
        _selectionInProgress = false;
        _selectionbox.sizeDelta = Vector2.zero;
        _selectionbox.gameObject.SetActive(false);

        if (Physics.Raycast(_camera.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hit, _unitLayerMask)
            && hit.collider.TryGetComponent<SelectableUnit>(out SelectableUnit unit))
        {
            if (Keyboard.current.leftShiftKey.isPressed)
            {
                if (_selectionManager.IsSelected(unit))
                {
                    _selectionManager.Deselect(unit);
                }
                else
                {
                    _selectionManager.Select(unit);
                }
            }
            else
            {
                _selectionManager.DeselectAll();
                _selectionManager.Select(unit);
            }
        }
        else if (_mouseDownTime + _dragDelay > Time.time)
        {
            _selectionManager.DeselectAll();
        }
        _mouseDownTime = 0;
    }

    private void OnStartSelection()
    {
        _selectionInProgress = true;
        _selectionbox.sizeDelta = Vector2.zero;
        _selectionbox.gameObject.SetActive(true);
        _startMousePosition = Mouse.current.position.ReadValue();
        _mouseDownTime = Time.time;
    }

    private void ResizeSelectionBox()
    {
        Vector2 currentMousePosition = Mouse.current.position.ReadValue();
        float width = currentMousePosition.x - _startMousePosition.x;
        float height = currentMousePosition.y - _startMousePosition.y;

        _selectionbox.anchoredPosition = _startMousePosition + new Vector2(width / 2f, height / 2f);
        _selectionbox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));

        Bounds bounds = new Bounds(_selectionbox.anchoredPosition, _selectionbox.sizeDelta);

        if (_selectionManager.AvailableUnits.Count > 0)
        {
            foreach (SelectableUnit unit in _selectionManager.AvailableUnits)
            {
                Vector2 unitScreenPosition = _camera.WorldToScreenPoint(unit.transform.position);
                if (UnitIsInSelectionBox(unitScreenPosition, bounds))
                {
                    _selectionManager.Select(unit);
                }
                else
                {
                    _selectionManager.Deselect(unit);
                }
            }
        }
    }

    private bool UnitIsInSelectionBox(Vector2 position, Bounds bounds)
    {
        return position.x > bounds.min.x && position.x < bounds.max.x
            && position.y > bounds.min.y && position.y < bounds.max.y;
    }

    private void SelectedMovementHandler()
    {
        if (_selectionManager.SelectedUnits.Count > 0)
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hit, _floorLayerMask))
            {
                foreach (SelectableUnit unit in _selectionManager.SelectedUnits)
                {
                    unit.MoveTo(hit.point);
                }
            }
        }
    }
}