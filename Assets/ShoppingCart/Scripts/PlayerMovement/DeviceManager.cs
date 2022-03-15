using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class DeviceManager : Singleton<DeviceManager>
{
    public InputDevice LeftHandDevice;
    public InputDevice RightHandDevice;

    private XRNode _leftHandXRNode = XRNode.LeftHand;
    private XRNode _rightHandXRNode = XRNode.RightHand;

    private List<InputDevice> _leftHandDevices = new List<InputDevice>();
    private List<InputDevice> _rightHandDevices = new List<InputDevice>();

    private void OnEnable()
    {
        if (!LeftHandDevice.isValid || !RightHandDevice.isValid)
        {
            GetDevices();
        }
    }

    private void Start()
    {
        if (!LeftHandDevice.isValid || !RightHandDevice.isValid)
        {
            GetDevices();
        }
    }

    private void GetDevices()
    {
        InputDevices.GetDevicesAtXRNode(_leftHandXRNode, _leftHandDevices);
        InputDevices.GetDevicesAtXRNode(_rightHandXRNode, _rightHandDevices);
        LeftHandDevice = _leftHandDevices.FirstOrDefault();
        RightHandDevice = _rightHandDevices.FirstOrDefault();
    }
}
