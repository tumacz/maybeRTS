using UnityEngine;

internal class BasicUnitSelectionResponse : MonoBehaviour, ISelectionResponse
{
    [SerializeField] private Renderer _renderer;
    private Color _defaultColor;

    private void Start()
    {
        if(_renderer != null)
        {
            _defaultColor = _renderer.material.color;
        }
    }

    public void OnSelectedResponse()
    {
        if(_renderer != null)
        {
            _renderer.material.color = Color.red;
        }
    }

    public void OnDeselectedResponse()
    {
        if(_renderer != null)
        {
            _renderer.material.color = _defaultColor;
        }
    }
}