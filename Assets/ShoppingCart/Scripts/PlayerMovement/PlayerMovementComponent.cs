using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class PlayerMovementComponent : MonoBehaviour
{
    private CharacterController _characterController;

    public GameObject AvatarHand;

    public Transform EndChargingTransform;

    private Transform _startChargingTransform;
    private Transform _lastHandTransform;

    public float SprintSpeedMultiplier;

    private float _handMoveTime = .0f;

    private bool _isHoldingTrigger;
    private bool _isCharging;

    private void Start()
    {
         _characterController = GetComponent<CharacterController>();

        _lastHandTransform = AvatarHand.transform;
    }

    private void Update()
    {
        _isHoldingTrigger = Input.GetKey(KeyCode.Space);
    }

    private void FixedUpdate()
    {
        SprintMove();
    }

    private void SprintMove()
    {
        bool isMovingDown = _lastHandTransform.position.y == AvatarHand.transform.position.y;
        Debug.Log(isMovingDown);

        if (_isHoldingTrigger)
        {
            //Conditions: 1.Hand Y Velocity < 0;    2.Moving Down;    3.Position upon Ending Position.
            if (isMovingDown && !_isCharging && AvatarHand.transform.position.y > EndChargingTransform.position.y)
            {
                _isCharging = true;
                _startChargingTransform = AvatarHand.transform;
                Debug.Log(_startChargingTransform.transform.position);
            }
        }

        _lastHandTransform = AvatarHand.transform;

        if (!_isCharging) return;

        _handMoveTime += Time.deltaTime;

        if (AvatarHand.transform.position.y > EndChargingTransform.position.y || _isHoldingTrigger) return;

        float acceleration = (_startChargingTransform.position.y - EndChargingTransform.position.y) / _handMoveTime;
        float chargingSpeed = acceleration * SprintSpeedMultiplier * Time.fixedDeltaTime;

        _characterController.Move(transform.forward * chargingSpeed);

        Debug.Log("Velocity: " + _characterController.GetComponent<Rigidbody>().velocity);

        Debug.Log("Move");

        _handMoveTime = 0;

        _isCharging = false;
    }
}
