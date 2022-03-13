using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public class SprintComponent : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public GameObject ForwardResource;
    public GameObject RightHandGameObject;
    public GameObject LeftHandGameObject;

    public Transform StartChargingTransform;
    public Transform EndChargingTransform;

    public float SprintSpeedMultiplier = 5f;
    public float FrictionRate = 0.5f;
    private float _localFrictionRate;

    private float _leftHandSprintTimer = 0f;
    private float _rightHandSprintTimer = 0f;

    private bool _isLeftHandSprint;
    private bool _isRightHandSprint;

    [SerializeField]
    private XRNode _leftHandButton;
    [SerializeField]
    private XRNode _rightHandButton;

    private List<InputDevice> _inputDevices = new List<InputDevice>();
    private InputDevice _rightHandInputDevice;
    private InputDevice _leftHandInputDevice;

    private bool _isLeftHandButtonPressed;
    private bool _isRightHandButtonPressed;

    #region Unity Methods
    private void OnEnable()
    {
        if (!_rightHandInputDevice.isValid || !_leftHandInputDevice.isValid)
        {
            GetDevice();
        }
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        if (!_rightHandInputDevice.isValid || !_leftHandInputDevice.isValid)
        {
            GetDevice();
        }

        _localFrictionRate = FrictionRate;
    }

    private void Update()
    {
        _leftHandInputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out _isLeftHandButtonPressed);
        _rightHandInputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out _isRightHandButtonPressed);

        Debug.Log(_isLeftHandButtonPressed || _isRightHandButtonPressed);
    }

    private void FixedUpdate()
    {
        if (!_rigidbody) return;
        MoveFriction();
        SprintMove(LeftHandGameObject, _isLeftHandButtonPressed, ref _leftHandSprintTimer, ref _isLeftHandSprint);
        SprintMove(RightHandGameObject, _isRightHandButtonPressed, ref _rightHandSprintTimer, ref _isRightHandSprint);
    }
    #endregion

    #region Custom Methods
    private void SprintMove(GameObject handObject, bool isPressedButton, ref float sprintTimer, ref bool isSprinting)
    {
        if (isPressedButton)
        {
            // Arrive Start Position, Always Set timer => 0
            if (handObject.transform.position.y >= StartChargingTransform.position.y)
            {
                sprintTimer = 0f;
                isSprinting = true;
                _localFrictionRate = 0f;
            }
        }

        // Smaller than Start Position, Start Timer
        if (handObject.transform.position.y >= StartChargingTransform.position.y || !isSprinting) return;

        sprintTimer += Time.deltaTime;

        // Arrive End Position, Accelerate Player According To Timer.
        if (handObject.transform.position.y >= EndChargingTransform.position.y) return;

        // Release TriggerButton to Activate Acceleration
        if (isPressedButton) return;

        // Acceleration Formula: 
        _rigidbody.velocity += ForwardResource.transform.forward * SprintSpeedMultiplier / sprintTimer * Time.fixedDeltaTime;

        isSprinting = false;

        _localFrictionRate = FrictionRate;
    }

    private void GetDevice()
    {
        InputDevices.GetDevicesAtXRNode(_leftHandButton, _inputDevices);
        _leftHandInputDevice = _inputDevices.FirstOrDefault();
        InputDevices.GetDevicesAtXRNode(_rightHandButton, _inputDevices);
        _rightHandInputDevice = _inputDevices.FirstOrDefault();
    }

    private void MoveFriction()
    {
        Vector3 horizontalSpeed = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
        float angle = Vector3.Dot(horizontalSpeed, transform.forward) / horizontalSpeed.magnitude;

        if (angle <= 0f || horizontalSpeed.magnitude <= 0.1f) return;

        _rigidbody.AddForce(-transform.forward * _localFrictionRate);
    }
    #endregion
}
