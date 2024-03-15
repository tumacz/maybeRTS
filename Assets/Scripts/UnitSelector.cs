using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting;

public class UnitSelector : MonoBehaviour, IControllerResponse
{
    [SerializeField] private UnitSelectionManager _unitSelectionManager;
    [SerializeField] private RectTransform _selectionbox;
    [SerializeField] private LayerMask _unitLayerMask;
    [SerializeField] private LayerMask _surfaceLayerMask;

    private Camera _camera;
    private IRayProvider _rayProvider;
    private Vector2 _startMousePosition;
    private float _mouseDownTime;
    private float _dragDelay = 0.1f;
    private bool _selectionInProgress = false;
    private bool _selectionExtended = false;

    private void Awake()
    {
        _rayProvider = GetComponent<IRayProvider>();
        _camera = _rayProvider.GetCamera();
    }

    private void Update()
    {

        if (_selectionInProgress)
        {
            ResizeSelectionBox();
        }
    }

    public void OnStartSelection()
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

        if (_unitSelectionManager.AvailableUnits.Count > 0)
        {
            foreach (ISelection unit in _unitSelectionManager.AvailableUnits)
            {
                Vector2 unitScreenPosition = _camera.WorldToScreenPoint(unit.CurrentPosition);
                bool isInSelectionBox = UnitIsInSelectionBox(unitScreenPosition, bounds);

                if (!_selectionExtended) //no extend selection response
                {
                    if (UnitIsInSelectionBox(unitScreenPosition, bounds))
                    {
                        _unitSelectionManager.Select(unit);
                        unit.OnHoverEnter();
                    }
                    else
                    {
                        _unitSelectionManager.Deselect(unit);
                        unit.OnHoverExit();
                    }
                }
                else //extended selection response
                {
                    if (UnitIsInSelectionBox(unitScreenPosition, bounds))
                    {
                        _unitSelectionManager.Select(unit);
                        unit.OnHoverEnter();
                    }
                }
            }
        }
    }

    public void OnEndSelection()
    {
        _selectionInProgress = false;
        _selectionbox.sizeDelta = Vector2.zero;
        _selectionbox.gameObject.SetActive(false);

        Ray ray = _rayProvider.CreateRayAtMousePosition();
        if (Physics.Raycast(ray, out RaycastHit hit, _unitLayerMask)
            && hit.collider.TryGetComponent<ISelection>(out ISelection unit))
        {
            if (_selectionExtended) //extend selection response
            {
                if (_unitSelectionManager.IsSelected(unit))
                {
                    _unitSelectionManager.Deselect(unit);
                    unit.OnDeselected();
                }
                else
                {
                    _unitSelectionManager.Select(unit);
                }
            }
            else //no extend selection response
            {
                _unitSelectionManager.DeselectAll();

                _unitSelectionManager.Select(unit);
            }
        }
        else if (_mouseDownTime + _dragDelay > Time.time) // deselection
        {
            _unitSelectionManager.DeselectAll();
        }
        _mouseDownTime = 0;
        _unitSelectionManager.DehoverAll();
        _unitSelectionManager.CheckSelected();
    }

    private bool UnitIsInSelectionBox(Vector2 position, Bounds bounds)
    {
        return position.x > bounds.min.x && position.x < bounds.max.x
            && position.y > bounds.min.y && position.y < bounds.max.y;
    }

    public void OnRightMouseButton()//redo
    {
        if (_unitSelectionManager.SelectedUnits.Count > 0)
        {
            Ray ray = _rayProvider.CreateRayAtMousePosition();
            if (Physics.Raycast(ray, out RaycastHit hit, _surfaceLayerMask))
            {
                foreach (ISelection unit in _unitSelectionManager.SelectedUnits)
                {
                    unit.Respond(hit.point);
                }
                foreach (ISelection unit in _unitSelectionManager.SelectedUnits)
                {
                    return;
                }
            }
        }
    }

    public void OnExtendSelectionStarted()
    {
        _selectionExtended = true;
    }

    public void OnExtendSelectionCanceled()
    {
        _selectionExtended = false;
    }
}