using UnityEngine;

public class PlayerInterractTool : MonoBehaviour, IControllerRightResponse
{
    [SerializeField] private UnitSelectionManager _unitSelectionManager;
    [SerializeField] private LayerMask _surfaceLayerMask;
    [SerializeField] private LayerMask _unitLayerMask;
    private IRayProvider _rayProvider;

    private void Awake()
    {
        _rayProvider = GetComponent<IRayProvider>();
    }

    public void OnRightMouseButton()
    {
        if (_unitSelectionManager.SelectedUnits.Count > 0)
        {
            Ray ray = _rayProvider.CreateRayAtMousePosition();
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                int layerHit = hit.collider.gameObject.layer;
                if (IsLayerInRange(layerHit) && (IsLayerInMask(layerHit, _surfaceLayerMask) || IsLayerInMask(layerHit, _unitLayerMask)))
                {
                    foreach (ISelection unit in _unitSelectionManager.SelectedUnits)
                    {
                        unit.Respond(hit.point, layerHit);
                    }
                }
                else
                {
                    Debug.Log("No specific response implemented for this layer. Layer name: " + LayerMask.LayerToName(layerHit));
                }
            }
            else
            {
                Debug.Log("No layer hit.");
            }
        }
        else
        {
            Debug.Log("No units selected.");
        }
    }

    private bool IsLayerInRange(int layer)
    {
        return layer >= 6 && layer <= 7;
    }

    private bool IsLayerInMask(int layer, LayerMask mask)
    {
        return (mask.value & (1 << layer)) != 0;
    }
}