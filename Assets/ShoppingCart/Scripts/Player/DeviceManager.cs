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
    
    private const float _INPUT_RECOVER_TIME = 3f;

    public bool CanPlayerInput { get; set; }

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

        CanPlayerInput = true;
    }

    private void GetDevices()
    {
        InputDevices.GetDevicesAtXRNode(_leftHandXRNode, _leftHandDevices);
        InputDevices.GetDevicesAtXRNode(_rightHandXRNode, _rightHandDevices);
        LeftHandDevice = _leftHandDevices.FirstOrDefault();
        RightHandDevice = _rightHandDevices.FirstOrDefault();
    }

    public void MutePlayerInput()
    {
        CanPlayerInput = false;
        
        Invoke(nameof(AllowPlayerInput) , _INPUT_RECOVER_TIME);
    }

    public void AllowPlayerInput()
    {
        CanPlayerInput = true;
    }

    public bool GetCanPlayerInput()
    {
        return CanPlayerInput;
    }
}
