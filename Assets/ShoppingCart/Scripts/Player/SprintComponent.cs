using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Scripts.Audio;
using ShoppingCart.Scripts.Player;
using UnityEngine;
using UnityEngine.XR;

public class SprintComponent : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;

    public Transform ForwardResource;
    public GameObject RightHandGameObject;
    public GameObject LeftHandGameObject;

    private bool _isLeftHandSprint;
    private bool _isRightHandSprint;

    private float _leftSprintTimer;
    private float _rightSprintTimer;

    private Vector3 _leftHandStartPos;
    private Vector3 _rightHandStartPos;

    private bool _isLeftHandButtonPressed;
    private bool _isRightHandButtonPressed;

    private InputComponent _inputComponent;

    [Space(5)]
    [Header("Velocity Arguments")]
    public float MaxMoveVelocity = 20f;
    public float CriticalDistance = 0.15f;

    #region Unity Methods
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();

        _leftHandStartPos = LeftHandGameObject.transform.position;
        _rightHandStartPos = RightHandGameObject.transform.position;

        _inputComponent = GetComponent<InputComponent>();
    }

    private void Update()
    {
        if(!_inputComponent.CanPlayerInput) return;
        
        DeviceManager.Instance.LeftHandDevice.TryGetFeatureValue(CommonUsages.triggerButton, out _isLeftHandButtonPressed);
        DeviceManager.Instance.RightHandDevice.TryGetFeatureValue(CommonUsages.triggerButton, out _isRightHandButtonPressed);
    }

    private void FixedUpdate()
    {
        if(!_inputComponent.CanPlayerInput) return;
        if (!_rigidbody) return;

        SprintMove(LeftHandGameObject, _isLeftHandButtonPressed, ref _isLeftHandSprint, ref _leftSprintTimer, ref _leftHandStartPos);
        SprintMove(RightHandGameObject, _isRightHandButtonPressed, ref _isRightHandSprint, ref _rightSprintTimer, ref _rightHandStartPos);

        TurnToForward();
    }
    #endregion

    #region Custom Methods
    private void SprintMove(GameObject handObject, bool isPressedButton, ref bool isSprinting, ref float timer, ref Vector3 startPosition)
    {
        // Press Button and Set Start Position
        if (isPressedButton)
        {
            startPosition = startPosition.y >= handObject.transform.position.y ? startPosition : handObject.transform.position;
            isSprinting = true;
            timer = 0f;
        }

        // Start Timer
        if (!isSprinting) return;
        timer += Time.deltaTime;

        // Release Button and Accelerate
        if (isPressedButton || handObject.transform.position.y >= startPosition.y) return;

        // Acceleration Formula: 
        if (_rigidbody.velocity.magnitude >= MaxMoveVelocity) return;
        var distance = startPosition.y - handObject.transform.position.y;
        if (distance <= CriticalDistance) return;
        var velocity = distance / timer;
        var acceleration = Mathf.Abs(velocity * (MaxMoveVelocity - _rigidbody.velocity.magnitude) / MaxMoveVelocity) * 30;
        _rigidbody.AddForce(ForwardResource.forward * acceleration);
        AudioInventory.Instance.PlayAudioClip(_audioSource, AudioInventory.AudioEnum.PlayerRun);
        isSprinting = false;
        startPosition = handObject.transform.position;
    }

    private void TurnToForward()
    {
        var forward = ForwardResource.forward;
        var angleCos = Vector3.Dot(_rigidbody.velocity, forward) / forward.magnitude / _rigidbody.velocity.magnitude;
        if (angleCos == 0) return;
        _rigidbody.velocity = ForwardResource.forward * _rigidbody.velocity.magnitude;
    }
    #endregion
}
