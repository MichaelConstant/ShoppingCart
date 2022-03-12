using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintComponent : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public GameObject ForwardResource;
    public GameObject RightHandGameObject;
    public GameObject LeftHandGameObject;

    public Transform StartChargingTransform;
    public Transform EndChargingTransform;

    public float SprintSpeedMultiplier = 5f;

    private float _leftHandSprintTimer = 0f;
    private float _rightHandSprintTimer = 0f;

    private bool _isLeftHandSprint;
    private bool _isRightHandSprint;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        SprintMove(LeftHandGameObject, ref _leftHandSprintTimer, ref _isLeftHandSprint);
        SprintMove(RightHandGameObject, ref _rightHandSprintTimer, ref _isRightHandSprint);
    }

    private void SprintMove(GameObject handObject, ref float sprintTimer, ref bool isSprinting)
    {
        // Arrive Start Position, Always Set timer => 0
        if (handObject.transform.position.y >= StartChargingTransform.position.y)
        {
            sprintTimer = 0f;
            isSprinting = true;
        }


        // Smaller than Start Position, Start Timer
        if (handObject.transform.position.y >= StartChargingTransform.position.y || !isSprinting) return;

        sprintTimer += Time.deltaTime;


        // Arrive End Position, Accelerate Player According To Timer.
        if (handObject.transform.position.y >= EndChargingTransform.position.y) return;

        _rigidbody.velocity += ForwardResource.transform.forward * SprintSpeedMultiplier / sprintTimer * Time.fixedDeltaTime;

        isSprinting = false;

        Debug.Log("Sprint!!!");
    }
}
