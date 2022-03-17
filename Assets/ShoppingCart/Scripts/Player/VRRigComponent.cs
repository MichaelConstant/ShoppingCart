using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VRMap
{
    public Transform VRTarget;
    public Transform RigTarget;
    public Vector3 TrackingPositionOffset;
    public Vector3 TrackingRotationOffset;

    public void Map()
    {
        RigTarget.position = VRTarget.TransformPoint(TrackingPositionOffset);
        RigTarget.rotation = VRTarget.rotation * Quaternion.Euler(TrackingRotationOffset);
    }
}

public class VRRigComponent : MonoBehaviour
{
    public VRMap Head;
    public VRMap LeftHand;
    public VRMap RightHand;

    public Transform HeadConstraint;
    public Vector3 BodyOffset;
    // private Vector3 _headBodyOffset;

    private void Start()
    {
        transform.position += BodyOffset;
        // _headBodyOffset = transform.position - HeadConstraint.position;
    }

    private void FixedUpdate()
    {
        // transform.position = HeadConstraint.position + _headBodyOffset;
        var headPosition = HeadConstraint.position;
        transform.position = new Vector3(headPosition.x, transform.position.y, headPosition.z);
        transform.forward = Vector3.ProjectOnPlane(-HeadConstraint.up, Vector3.up).normalized;

        Head.Map();
        LeftHand.Map();
        RightHand.Map();
    }
}