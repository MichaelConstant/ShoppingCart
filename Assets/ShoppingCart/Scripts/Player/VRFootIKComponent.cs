using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRFootIKComponent : MonoBehaviour
{
    private Animator _animator;

    public Vector3 FootOffset;

    public float LeftFootPosWeight = 1;
    public float LeftFootRotWeight = 1;
    public float RightFootPosWeight = 1;
    public float RightFootRotWeight = 1;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        var rightFootPos = _animator.GetIKPosition(AvatarIKGoal.RightFoot);
        var isHit = Physics.Raycast(rightFootPos + Vector3.up, Vector3.down, out var hitInfo);
        if (isHit)
        {
            _animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, RightFootPosWeight);
            _animator.SetIKPosition(AvatarIKGoal.RightFoot, hitInfo.point + FootOffset);

            var footRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hitInfo.normal),
                hitInfo.normal);
            _animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, RightFootRotWeight);
            _animator.SetIKRotation(AvatarIKGoal.RightFoot, footRotation);
        }
        else
        {
            _animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
        }

        var leftFootPos = _animator.GetIKPosition(AvatarIKGoal.LeftFoot);
        isHit = Physics.Raycast(leftFootPos + Vector3.up, Vector3.down, out hitInfo);
        if (isHit)
        {
            _animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, LeftFootPosWeight);
            _animator.SetIKPosition(AvatarIKGoal.LeftFoot, hitInfo.point + FootOffset);

            var footRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hitInfo.normal),
                hitInfo.normal);
            _animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, LeftFootRotWeight);
            _animator.SetIKRotation(AvatarIKGoal.LeftFoot, footRotation);
        }
        else
        {
            _animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
        }
    }
}