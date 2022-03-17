using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BrakeComponent : MonoBehaviour
{
    public float BrakeVelocity = 2f;
    private const float SYNERGY_MULTIPLIER = 1.5f;

    public Transform ForwardResource;
    
    private Rigidbody _rigidbody;

    private bool _isPressedLeftButton;
    private bool _isPressedRightButton;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        DeviceManager.Instance.LeftHandDevice.TryGetFeatureValue(CommonUsages.primaryButton, out _isPressedLeftButton);
        DeviceManager.Instance.RightHandDevice.TryGetFeatureValue(CommonUsages.primaryButton, out _isPressedRightButton);
    }

    private void FixedUpdate()
    {
        if (!_rigidbody) return;

        var brakeVelocity = BrakeVelocity;

        if (!_isPressedLeftButton || !_isPressedRightButton || _rigidbody.velocity.magnitude <= 3f) return;

        if (_isPressedLeftButton && _isPressedRightButton)
        {
            brakeVelocity *= SYNERGY_MULTIPLIER;
        }

        _rigidbody.velocity -= ForwardResource.forward * brakeVelocity;
    }
}
