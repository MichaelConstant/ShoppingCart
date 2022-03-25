using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerNetworkSetupComponent : MonoBehaviourPunCallbacks
{
    public GameObject LocalXROriginGameObject;
    public GameObject AvatarModelGameObject;
    private void Start()
    {
        if (photonView.IsMine)
        {
            // The player is LOCAL
            LocalXROriginGameObject.SetActive(true);
            
            SetLayerRecursively(AvatarModelGameObject, 9);
        }
        else
        {
            // The player is REMOTE
            LocalXROriginGameObject.SetActive(false);
            
            SetLayerRecursively(AvatarModelGameObject, 7);
        }
    }

    private void SetLayerRecursively(GameObject localGameObject, int layerNumber)
    {
        if (localGameObject == null) return;
        
        foreach (var localTransform in localGameObject.GetComponentsInChildren<Transform>(true))
        {
            localTransform.gameObject.layer = layerNumber;
        }
    }
}
