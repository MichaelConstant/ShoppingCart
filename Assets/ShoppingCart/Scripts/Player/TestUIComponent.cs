using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestUIComponent : MonoBehaviour
{
    public TextMeshProUGUI VelocityText;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (VelocityText)
        {
            VelocityText.text = "Velocity: " + _rigidbody.velocity.z;
        }
    }
}
