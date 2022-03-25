using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

[Serializable]
public enum DeviceName
{
    LeftHand,
    RightHand,
}

public class RayComponent : MonoBehaviour
{
    public DeviceName MyDevice;
    
    private LineRenderer _lineRenderer;
    private XRRayInteractor _xrRayInteractor;
    private XRInteractorLineVisual _interactorLineVisual;

    private bool _isPressedButton;
    
    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _xrRayInteractor = GetComponent<XRRayInteractor>();
        _interactorLineVisual = GetComponent<XRInteractorLineVisual>();
    }

    private void Update()
    {
        switch (MyDevice)
        {
            case DeviceName.LeftHand:
                DeviceManager.Instance.LeftHandDevice.TryGetFeatureValue(CommonUsages.gripButton, out _isPressedButton);
                break;
            case DeviceName.RightHand:
                DeviceManager.Instance.RightHandDevice.TryGetFeatureValue(CommonUsages.gripButton, out _isPressedButton);
                break;
            default:
                return;
        }

        var ray = new Ray(transform.position, transform.forward);

        if (!Physics.Raycast(ray, out var hitInfo, _xrRayInteractor.maxRaycastDistance)) return;
        
        if (!hitInfo.collider.GetComponent<GoodComponent>()) return;

        
        // if (!_interactorLineVisual || !_lineRenderer) return;
        //_interactorLineVisual.enabled = _isPressedButton;
        // _lineRenderer.enabled = _isPressedButton;
    }
}
