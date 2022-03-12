using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintComponent : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public GameObject RightHandGameObject;
    public GameObject LeftHandGameObject;

    public Transform StartChargingTransform;
    public Transform EndChargingTransform;

    public float SprintSpeedMultiplier = 10f;

    private float SprintTimer = 0f;

    private bool isSprinting;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Arrive Start Position, Always Set timer => 0
        if (RightHandGameObject.transform.position.y >= StartChargingTransform.position.y)
        {
            SprintTimer = 0f;
            isSprinting = true;
        }

        // Smaller than Start Position, Start Timer
        if (RightHandGameObject.transform.position.y >= StartChargingTransform.position.y || !isSprinting) return;

        SprintTimer += Time.deltaTime;

        // Arrive End Position, Accelerate Player According To Timer.
        if (RightHandGameObject.transform.position.y >= EndChargingTransform.position.y) return;

        _rigidbody.velocity += transform.forward * SprintSpeedMultiplier / SprintTimer * Time.fixedDeltaTime;

        isSprinting = false;
    }
}
