using System;
using System.Collections;
using System.Collections.Generic;
using ShoppingCart.Scripts.Goods;
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
    
    public ScoreComponent ScoreComponent;

    public bool CanPurchase => canPurchase;

    private XRRayInteractor _xrRayInteractor;
    
    private bool _isPressedButton;
    private bool canPurchase;
    private void Start()
    {
        _xrRayInteractor = GetComponent<XRRayInteractor>();
    }

    private void Update()
    {
        switch (MyDevice)
        {
            case DeviceName.LeftHand:
                DeviceManager.Instance.LeftHandDevice.TryGetFeatureValue(CommonUsages.gripButton, out _isPressedButton);
                break;
            case DeviceName.RightHand:
                DeviceManager.Instance.RightHandDevice.TryGetFeatureValue(CommonUsages.gripButton,
                    out _isPressedButton);
                break;
            default:
                return;
        }

        if (!_isPressedButton) return;

        var ray = new Ray(transform.position, transform.forward);

        if (!Physics.Raycast(ray, out var hitInfo, _xrRayInteractor.maxRaycastDistance)) return;

        if (!hitInfo.collider.GetComponent<GoodComponent>()) return;

        canPurchase = true;
        
        Debug.Log("CanPurchased");
    }
}