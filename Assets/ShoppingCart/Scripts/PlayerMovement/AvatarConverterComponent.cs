using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarConverterComponent : MonoBehaviour
{
    [Header("Avatar Transforms")]

    public Transform MainAvatarTransform;
    public Transform AvatarHead;
    public Transform AvatarBody;

    public Transform AvatarLeftHand;
    public Transform AvatarRightHand;

    [Space(5)]

    [Header("XR Origin Transforms")]

    public Transform XRHead;
    public Transform XRLeftHand;
    public Transform XRRightHand;

    public Vector3 HeadPositionOffset;
    public Vector3 HandRotationOffset;

    private void Update()
    {
        MainAvatarTransform.position = Vector3.Lerp(MainAvatarTransform.position, XRHead.position + HeadPositionOffset, 0.5f);
        AvatarHead.rotation = Quaternion.Lerp(AvatarHead.rotation, XRHead.rotation, 0.5f);
        AvatarBody.rotation = Quaternion.Lerp(AvatarBody.rotation, Quaternion.Euler(new Vector3(0, AvatarHead.rotation.eulerAngles.y, 0)), 0.05f);

        AvatarRightHand.position = Vector3.Lerp(AvatarRightHand.position, XRRightHand.position, 0.5f);
        AvatarRightHand.rotation = Quaternion.Lerp(AvatarRightHand.rotation, XRRightHand.rotation, 0.5f) * Quaternion.Euler(HandRotationOffset);

        AvatarLeftHand.position = Vector3.Lerp(AvatarLeftHand.position, XRLeftHand.position, 0.5f);
        AvatarLeftHand.rotation = Quaternion.Lerp(AvatarLeftHand.rotation, XRLeftHand.rotation, 0.5f) * Quaternion.Euler(HandRotationOffset);
    }
}
