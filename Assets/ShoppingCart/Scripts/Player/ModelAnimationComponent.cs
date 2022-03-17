using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelAnimationComponent : MonoBehaviour
{
    public GameObject XROrigin;

    private Rigidbody _rigidbody;
    private Animator _animator;
    private static readonly int RightVelocity = Animator.StringToHash("RightVelocity");
    private static readonly int ForwardVelocity = Animator.StringToHash("ForwardVelocity");

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = XROrigin.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _animator.SetFloat(RightVelocity, _rigidbody.velocity.x);
        _animator.SetFloat(ForwardVelocity, _rigidbody.velocity.z);
    }
}
