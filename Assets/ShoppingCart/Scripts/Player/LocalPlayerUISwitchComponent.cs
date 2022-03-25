using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class LocalPlayerUISwitchComponent : MonoBehaviour
{
    public GameObject UIHandControllerGameObject;
    public GameObject PurchaseHandControllerGameObject;

    public GameObject LocalPlayerUIGameObject;
    public DeviceName MyDevice;

    private bool _isPressedButton;

    private void Start()
    {
        if (!LocalPlayerUIGameObject) return;
        LocalPlayerUIGameObject.SetActive(false);

        UIHandControllerGameObject.SetActive(false);
        PurchaseHandControllerGameObject.SetActive(true);
    }

    private void Update()
    {
        UIHandControllerGameObject.SetActive(LocalPlayerUIGameObject.activeSelf);
        PurchaseHandControllerGameObject.SetActive(!LocalPlayerUIGameObject.activeSelf);


        var previousButtonPressed = _isPressedButton;
        switch (MyDevice)
        {
            case DeviceName.LeftHand:
                DeviceManager.Instance.LeftHandDevice.TryGetFeatureValue(CommonUsages.secondaryButton,
                    out _isPressedButton);
                break;
            case DeviceName.RightHand:
                DeviceManager.Instance.RightHandDevice.TryGetFeatureValue(CommonUsages.secondaryButton,
                    out _isPressedButton);
                break;
            default:
                return;
        }

        if (previousButtonPressed == _isPressedButton) return;

        if (!_isPressedButton || !LocalPlayerUIGameObject) return;

        LocalPlayerUIGameObject.SetActive(!LocalPlayerUIGameObject.activeSelf);
    }
}