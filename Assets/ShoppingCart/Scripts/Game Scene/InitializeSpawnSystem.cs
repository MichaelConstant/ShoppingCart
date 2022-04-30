using System;
using Photon.Pun;
using UnityEngine;

namespace ShoppingCart.Scripts.Game_Scene
{
    public class InitializeSpawnSystem : MonoBehaviourPun
    {
        [SerializeField] private GameObject PlayerSpawnSystem;
        [SerializeField] private GameObject GameOverSystem;
        
        private void Awake()
        {
            PhotonNetwork.Instantiate(PlayerSpawnSystem.name, Vector3.zero, Quaternion.identity);
            PhotonNetwork.Instantiate(GameOverSystem.name, Vector3.zero, Quaternion.identity);
        }
    }
}