using System;
using UnityEngine;

public class Calculator : MonoBehaviour
{
    public Material screenOne;
    public Material screenZero;

    private MeshRenderer _screenRenderer;
    public Vector3 EctsPosition { get; set; } = Vector3.zero;

    private void OnEnable()
    {
        _screenRenderer = transform.Find("model").Find("Screen Quad").GetComponent<MeshRenderer>();
    }

    private bool _isScreenOne = false;

    private void Update()
    {
        var dotProduct = Vector3.Dot(EctsPosition.normalized, -transform.forward);
        if (dotProduct > 0.7f && dotProduct < 1f && !_isScreenOne)
        {
            _screenRenderer.material = screenOne;
            _isScreenOne = true;
        }
        else if (dotProduct < 0.7f && _screenRenderer.material != screenZero && _isScreenOne)
        {
            _screenRenderer.material = screenZero;
            _isScreenOne = false;
        }
    }
}