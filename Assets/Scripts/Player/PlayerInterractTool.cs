using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerInterractTool : MonoBehaviour, IControllerRightResponse
{
    [SerializeField] private UnitSelectionManager _unitSelectionManager;
    [SerializeField] private List<LayerMask> _interactableLayerMasks;
    [SerializeField] private LayerMask _ignoreRaycastLayerMask;

    private IRayProvider _rayProvider;
    private List<int> _interactableLayersInRange = new List<int>();
    private Vector2 _interactableLayerRange;
    private void Awake()
    {
        _rayProvider = GetComponent<IRayProvider>();
    }

    private void Start()
    {
        ExtrudeLayerNumbers(_interactableLayerMasks);
        _interactableLayerRange = CreateLayerRange(_interactableLayersInRange);
    }

    private void ExtrudeLayerNumbers(List<LayerMask> list)
    {
        foreach (LayerMask layerMask in list)
        {
            int layerNumber = ExtractLayerNumber(layerMask);
            _interactableLayersInRange.Add(layerNumber);
        }
    }

    private int ExtractLayerNumber(LayerMask layerMask)
    {
        int layerNumber = Mathf.RoundToInt(Mathf.Log(layerMask.value, 2));
        return layerNumber;
    }

    private Vector2 CreateLayerRange(List<int> list)
    {
        int min = _interactableLayersInRange.Min();
        int max = _interactableLayersInRange.Max();
        return new Vector2(min, max);
    }

    public void OnRightMouseButton()
    {
        if (_unitSelectionManager.SelectedUnits.Count == 0)
        {
            Debug.Log("No units selected.");
            return;
        }

        Ray ray = _rayProvider.CreateRayAtMousePosition();
        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~_ignoreRaycastLayerMask))
        {
            Debug.Log("No layer hit.");
            return;
        }

        int layerHit = hit.collider.gameObject.layer;
        GameObject hitObject = hit.collider.gameObject;
        if (!IsLayerInRange(layerHit))
        {
            Debug.LogWarning("Layer out of range. Layer name: " + LayerMask.LayerToName(layerHit));
            return;
        }

        bool isInteractable = false;
        foreach (var interactableLayerMask in _interactableLayerMasks)
        {
            if (IsLayerInMask(layerHit, interactableLayerMask))
            {
                isInteractable = true;
                break;
            }
        }

        if (!isInteractable)
        {
            Debug.Log("No specific response implemented for this layer. Layer name: " + LayerMask.LayerToName(layerHit));
            return;
        }

        foreach (var unit in _unitSelectionManager.SelectedUnits)
        {
            unit.Respond(hitObject, hit.point);
        }
    }

    private bool IsLayerInRange(int layer)
    {
        return layer >= _interactableLayerRange.x && layer <= _interactableLayerRange.y;
    }

    private bool IsLayerInMask(int layer, LayerMask mask)
    {
        return (mask.value & (1 << layer)) != 0;
    }
}