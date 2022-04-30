using System;
using Photon.Pun;
using UnityEngine;

namespace ShoppingCart.Scripts.Game_Scene
{
    public class InitializeSpawnSystem : MonoBehaviourPun
    {
        [SerializeField] private GameObject PlayerSpawnSystem;
        private void Awake()
        {
            PhotonNetwork.Instantiate(PlayerSpawnSystem.name, Vector3.zero, Quaternion.identity);
        }
    }
}