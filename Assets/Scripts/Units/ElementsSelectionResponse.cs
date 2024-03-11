using UnityEngine;
using UnityEngine.InputSystem;

public class ElementsSelectionResponse : MonoBehaviour, IResponse
{
    [SerializeField] private UnitSelectionManager _unitSelectionManager;
    [SerializeField] private RectTransform _selectionbox;
    [SerializeField] private LayerMask _unitLayerMask;
    [SerializeField] private LayerMask _floorLayerMask;
    [SerializeField] private Camera _camera;

    private IRayProvider _rayProvider;
    private Vector2 _startMousePosition;
    private float _mouseDownTime;
    private float _dragDelay = 0.1f;
    private bool _selectionInProgress = false;

    private void Awake()
    {
        _rayProvider = GetComponent<IRayProvider>();
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
                if(unit is MonoBehaviour monoBehaviour)
                {
                    Vector2 unitScreenPosition = _camera.WorldToScreenPoint(monoBehaviour.transform.position);
                    if (UnitIsInSelectionBox(unitScreenPosition, bounds))
                    {
                        _unitSelectionManager.Select(unit);
                    }
                    else
                    {
                        _unitSelectionManager.Deselect(unit);
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
            //if (Keyboard.current.leftShiftKey.isPressed)//redo to new input
            //{
            //    if (_unitSelectionManager.IsSelected(unit))
            //    {
            //        _unitSelectionManager.Deselect(unit);
            //    }
            //    else
            //    {
            //        _unitSelectionManager.Select(unit);
            //    }
            //}
            //else
            //{
                _unitSelectionManager.DeselectAll();
                _unitSelectionManager.Select(unit);
            //}
        }
        else if (_mouseDownTime + _dragDelay > Time.time)
        {
            _unitSelectionManager.DeselectAll();
        }
        _mouseDownTime = 0;
    }

    private bool UnitIsInSelectionBox(Vector2 position, Bounds bounds)
    {
        return position.x > bounds.min.x && position.x < bounds.max.x
            && position.y > bounds.min.y && position.y < bounds.max.y;
    }

    public void OnRightMouseButton()
    {
        if (_unitSelectionManager.SelectedUnits.Count > 0)
        {
            Ray ray = _rayProvider.CreateRayAtMousePosition();
            if (Physics.Raycast(ray, out RaycastHit hit, _floorLayerMask))
            {
                foreach (SelectableUnit unit in _unitSelectionManager.SelectedUnits)
                {
                    unit.MoveTo(hit.point);
                }
            }
        }
        if(_unitSelectionManager.SelectedBuildings.Count > 0)
        {
            foreach(SelectableBuilding building in _unitSelectionManager.SelectedBuildings)
                building.OnDeselected();//
        }
    }
}