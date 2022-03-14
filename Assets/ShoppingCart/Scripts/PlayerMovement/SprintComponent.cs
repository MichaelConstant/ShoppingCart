using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public class SprintComponent : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public Transform ForwardResource;
    public GameObject RightHandGameObject;
    public GameObject LeftHandGameObject;

    private bool _isLeftHandSprint;
    private bool _isRightHandSprint;

    private float _leftSprintTimer;
    private float _rightSprintTimer;

    private Vector3 _leftHandStartPos;
    private Vector3 _rightHandStartPos;

    [SerializeField]
    private XRNode _leftHandButton;
    [SerializeField]
    private XRNode _rightHandButton;

    private List<InputDevice> _leftInputDevices = new List<InputDevice>();
    private List<InputDevice> _rightInputDevices = new List<InputDevice>();
    private InputDevice _rightHandInputDevice;
    private InputDevice _leftHandInputDevice;

    private bool _isLeftHandButtonPressed;
    private bool _isRightHandButtonPressed;

    [Space(5)]
    [Header("Velocity Arguments")]
    public float MaxMoveVelocity = 5f;
    public float FrictionRate = 0.5f;
    public float CriticalStopVelocity = 0.5f;

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

        _leftHandStartPos = LeftHandGameObject.transform.position;
        _rightHandStartPos = RightHandGameObject.transform.position;
    }

    private void Update()
    {
        _leftHandInputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out _isLeftHandButtonPressed);
        _rightHandInputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out _isRightHandButtonPressed);
    }

    private void FixedUpdate()
    {
        if (!_rigidbody) return;

        SprintMove(LeftHandGameObject, _isLeftHandButtonPressed, ref _isLeftHandSprint, ref _leftSprintTimer, ref _leftHandStartPos);
        SprintMove(RightHandGameObject, _isRightHandButtonPressed, ref _isRightHandSprint, ref _rightSprintTimer, ref _rightHandStartPos);

        TurnToForward();
    }
    #endregion

    #region Custom Methods
    private void SprintMove(GameObject handObject, bool isPressedButton, ref bool isSprinting, ref float timer, ref Vector3 startPosition)
    {
        /// Press Button and Set Start Position
        /// 
        if (isPressedButton)
        {
            startPosition = startPosition.y >= handObject.transform.position.y ? startPosition : handObject.transform.position;
            isSprinting = true;
            timer = 0f;
        }

        /// Start Timer
        /// 
        if (!isSprinting) return;
        timer += Time.deltaTime;

        /// Release Button and Accelerate
        /// 
        if (isPressedButton || handObject.transform.position.y >= startPosition.y) return;

        /// Acceleration Formula: 
        /// 
        /// define the acceleration
        /// 
        float distance = startPosition.y - handObject.transform.position.y;
        float velocity = distance / timer;
        float acceleration = Mathf.Abs(velocity * (MaxMoveVelocity - _rigidbody.velocity.magnitude) / MaxMoveVelocity) * 30;

        if (_rigidbody.velocity.magnitude >= MaxMoveVelocity) return;
        _rigidbody.AddForce(ForwardResource.forward * acceleration);
        //_rigidbody.velocity = _rigidbody.velocity.magnitude >= MaxMoveVelocity ? _rigidbody.velocity : _rigidbody.velocity + ForwardResource.forward * acceleration;

        isSprinting = false;
    }

    private void GetDevice()
    {
        InputDevices.GetDevicesAtXRNode(_leftHandButton, _leftInputDevices);
        _leftHandInputDevice = _leftInputDevices.FirstOrDefault();
        InputDevices.GetDevicesAtXRNode(_rightHandButton, _rightInputDevices);
        _rightHandInputDevice = _rightInputDevices.FirstOrDefault();
    }

    private void TurnToForward()
    {
        float angleCos = Vector3.Dot(_rigidbody.velocity, ForwardResource.forward) / ForwardResource.forward.magnitude / _rigidbody.velocity.magnitude;
        if (angleCos == 0) return;
        _rigidbody.velocity = ForwardResource.forward * _rigidbody.velocity.magnitude;
    }
    #endregion
}
