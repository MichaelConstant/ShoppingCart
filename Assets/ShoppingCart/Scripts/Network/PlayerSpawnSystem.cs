using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ShoppingCart.Scripts.Network;
using Unity.Mathematics;

public class PlayerSpawnSystem : MonoBehaviour
{
    [SerializeField] GameObject _genericVRPlayerPrefab;

    public Vector3 SpawnPosition;

    private void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.Instantiate(_genericVRPlayerPrefab.name, SpawnPosition, Quaternion.identity);
            FindObjectOfType<PlayersInitializeSystem>().InitializeGameActors();
        }
    }
}