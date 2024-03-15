using UnityEngine;

internal class BasicBuldingSelectionResponse : MonoBehaviour, ISelectionResponse
{
    [SerializeField] private Renderer _renderer;
    private Color _defaultColor;

    private void Start()
    {
        if (_renderer != null)
        {
            _defaultColor = _renderer.material.color;
        }
    }

    public void OnSelectedResponse()
    {
        if (_renderer != null)
        {
            _renderer.material.color = Color.yellow;
        }
    }

    public void OnDeselectedResponse()
    {
        if (_renderer != null)
        {
            _renderer.material.color = _defaultColor;
        }
    }

    public void OnHoverEnterResponse()
    {
        if (_renderer != null)
        {
            _renderer.material.color = Color.black;
        }
    }

    public void OnHoverExitResponse()
    {
        if (_renderer != null)
        {
            _renderer.material.color = _defaultColor;
        }
    }
}