using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public class TestScript : MonoBehaviour
{
    [SerializeField]
    private XRNode _xrNode;

    private List<InputDevice> _inputDevices = new List<InputDevice>();

    private InputDevice _inputDevice;

    private void OnEnable()
    {
        if (!_inputDevice.isValid)
        {
            GetDevice();
        }
    }

    private void Update()
    {
        if (!_inputDevice.isValid)
        {
            GetDevice();
        }

        bool isTriggerButtonPressed = false;

        if (_inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out isTriggerButtonPressed) && isTriggerButtonPressed)
        {
            Debug.Log(isTriggerButtonPressed);
        }

    }

    private void GetDevice()
    {
        InputDevices.GetDevicesAtXRNode(_xrNode, _inputDevices);
        _inputDevice = _inputDevices.FirstOrDefault();
    }
}
